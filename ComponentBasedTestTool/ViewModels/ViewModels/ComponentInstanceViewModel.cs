using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CallMeMaybe;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
using ViewModels.Composition;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels
{
  public class ComponentInstanceViewModel : 
    INotifyPropertyChanged, 
    TestComponentContext, 
    TestComponentOperationDestination
  {
    private string _instanceName;
    private readonly OutputFactory _outputFactory;
    private OperationViewModels _operationViewModels;
    private readonly OperationEntries _operationEntries;
    private readonly TestComponent _testComponentInstance;
    private readonly BackgroundTasks _backgroundTasks;
    private readonly OperationMachinesByControlObject _operationMachinesByControlObject;

    public ComponentInstanceViewModel(
      string instanceName, 
      OutputFactory outputFactory, 
      OperationEntries operationEntries, 
      TestComponent testComponentInstance, 
      BackgroundTasks backgroundTasks, 
      OperationMachinesByControlObject operationMachinesByControlObject)
    {
      _instanceName = instanceName;
      _outputFactory = outputFactory;
      _operationEntries = operationEntries;
      _testComponentInstance = testComponentInstance;
      _backgroundTasks = backgroundTasks;
      _operationMachinesByControlObject = operationMachinesByControlObject;
    }

    public void Initialize(OperationViewModelFactory operationViewModelFactory)
    {
      _testComponentInstance.CreateOperations(this);
      _testComponentInstance.PopulateOperations(this);
      _operationViewModels = _operationEntries.ConvertUsing(operationViewModelFactory);
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

    public void AddOperationsTo(OperationsViewModel operationsViewModel)
    {
      _operationViewModels.AddTo(operationsViewModel);
    }

    public void AddOperation(string name, OperationControl operation, string dependencyName)
    {
      _operationEntries.Add(name, _operationMachinesByControlObject.For(operation), Maybe.From(dependencyName));
    }

    public void AddOperation(string name, OperationControl operation)
    {
      _operationEntries.Add(name, _operationMachinesByControlObject.For(operation), Maybe.Not);
    }

    public OperationsOutput CreateOutFor(string operationName)
    {
      return _outputFactory.CreateOutFor(operationName);
    }

    public OperationControl CreateOperation(ComponentOperation operation)
    {
      var defaultOperationStateMachine = StateMachineFor(operation, _backgroundTasks);
      _operationMachinesByControlObject.Register(defaultOperationStateMachine);
      return defaultOperationStateMachine;
    }

    //bug move to factory
    private static DefaultOperationStateMachine StateMachineFor(ComponentOperation componentOperation, BackgroundTasks backgroundTasks)
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

    public ICommand ShowCustomUiForComponentInstanceCommand => new ShowCustomUiForComponentInstanceCommand(_testComponentInstance);

  }
}
