using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using Core.Maybe;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
using ViewModels.Composition;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels;

public class ComponentInstanceViewModel : 
  INotifyPropertyChanged, 
  ITestComponentContext, 
  ITestComponentOperationDestination
{
  private string _instanceName;
  private readonly OutputFactory _outputFactory;
  private OperationViewModelsSource _operationViewModelsSource;
  private readonly OperationEntries _operationEntries;
  private readonly ICoreTestComponent _testComponentInstance;
  private readonly Capabilities.ICustomGui _customUi;
  private readonly IBackgroundTasks _backgroundTasks;
  private readonly OperationMachinesByControlObject _operationMachinesByControlObject;

  public ComponentInstanceViewModel(
    string instanceName, 
    OutputFactory outputFactory, 
    OperationEntries operationEntries, 
    ICoreTestComponent testComponentInstance, 
    IBackgroundTasks backgroundTasks, 
    OperationMachinesByControlObject operationMachinesByControlObject, 
    Capabilities.ICustomGui customUi)
  {
    _instanceName = instanceName;
    _outputFactory = outputFactory;
    _operationEntries = operationEntries;
    _testComponentInstance = testComponentInstance;
    _customUi = customUi;
    _backgroundTasks = backgroundTasks;
    _operationMachinesByControlObject = operationMachinesByControlObject;
  }

  public void Initialize(IOperationViewModelFactory operationViewModelFactory)
  {
    _testComponentInstance.CreateOperations(this);
    _testComponentInstance.PopulateOperations(this);
    _operationViewModelsSource = _operationEntries.ConvertUsing(operationViewModelFactory);
  }

  public string InstanceName
  {
    get { return _instanceName; }
    set
    {
      _instanceName = value;
      OnPropertyChanged();
    }
  }

  public event PropertyChangedEventHandler PropertyChanged;

  [NotifyPropertyChangedInvocator]
  protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  public void UpdateOperationsOn(OperationsViewModel operationsViewModel)
  {
    _operationViewModelsSource.AddTo(operationsViewModel);
  }

  public void AddOperation(string name, IOperationControl operation, string dependencyName)
  {
    _operationEntries.Add(InstanceName, name, _operationMachinesByControlObject.For(operation), dependencyName.ToMaybe());
  }

  public void AddOperation(string name, IOperationControl operation)
  {
    _operationEntries.Add(InstanceName, name, _operationMachinesByControlObject.For(operation), Maybe<string>.Nothing);
  }

  public IOperationsOutput CreateOutFor(string operationName)
  {
    return _outputFactory.CreateOutFor(operationName);
  }

  public IOperationControl CreateOperation(IComponentOperation operation)
  {
    var defaultOperationStateMachine = StateMachineFor(operation, _backgroundTasks);
    _operationMachinesByControlObject.Register(defaultOperationStateMachine);
    return defaultOperationStateMachine;
  }

  //bug move to factory
  private static DefaultOperationStateMachine StateMachineFor(IComponentOperation componentOperation, IBackgroundTasks backgroundTasks)
  {
    return new DefaultOperationStateMachine(
      componentOperation,
      new UnavailableOperationState(),
      new OperationStatesFactory(backgroundTasks));
  }


  public void SaveTo(PersistentModelFileContentBuilder persistentModelFileContentBuilder)
  {
    persistentModelFileContentBuilder.NewComponentInstance(this.InstanceName, _testComponentInstance);
    _testComponentInstance.PopulateOperations(persistentModelFileContentBuilder);
  }

  public ICommand ShowCustomUiForComponentInstanceCommand => new ShowCustomUiForComponentInstanceCommand(_customUi);

}