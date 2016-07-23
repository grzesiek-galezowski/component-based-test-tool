using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CallMeMaybe;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
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

    public ComponentInstanceViewModel(
      string instanceName, 
      OutputFactory outputFactory, 
      OperationEntries operationEntries, 
      TestComponent testComponentInstance, 
      BackgroundTasks backgroundTasks)
    {
      _instanceName = instanceName;
      _outputFactory = outputFactory;
      _operationEntries = operationEntries;
      _testComponentInstance = testComponentInstance;
      _backgroundTasks = backgroundTasks;
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

    public void AddOperation(string name, OperationStateMachine operation, string dependencyName)
    {
      _operationEntries.Add(name, operation, Maybe.From(dependencyName));
    }

    public void AddOperation(string name, OperationStateMachine operation)
    {
      _operationEntries.Add(name, operation, Maybe.Not);
    }

    public OperationsOutput CreateOutFor(string operationName)
    {
      return _outputFactory.CreateOutFor(operationName);
    }

    public OperationStateMachine CreateOperation(ComponentOperation operation)
    {
      return StateMachineFor(operation, _backgroundTasks);
    }

    //bug move to factory
    private static DefaultOperationStateMachine StateMachineFor(ComponentOperation componentOperation, BackgroundTasks backgroundTasks)
    {
      return new DefaultOperationStateMachine(
        componentOperation,
        new UnavailableOperationState(),
        new OperationStatesFactory(backgroundTasks));
    }


    public void SaveTo(FileBasedPersistentStorage fileBasedPersistentStorage)
    {
      fileBasedPersistentStorage.NewComponentInstance(this.InstanceName, _testComponentInstance);
      _testComponentInstance.PopulateOperations(fileBasedPersistentStorage);
    }

    public ICommand ShowCustomUiForComponentInstanceCommand => new ShowCustomUiForComponentInstanceCommand(_testComponentInstance);

  }

  public class ShowCustomUiForComponentInstanceCommand : ICommand
  {
    private readonly TestComponent _testComponentInstance;

    public ShowCustomUiForComponentInstanceCommand(TestComponent testComponentInstance)
    {
      _testComponentInstance = testComponentInstance;
    }

    public bool CanExecute(object parameter)
    {
      return true; //for now
    }

    public void Execute(object parameter)
    {
      _testComponentInstance.ShowCustomUi();
    }

    public event EventHandler CanExecuteChanged;

    protected virtual void OnCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }

}
