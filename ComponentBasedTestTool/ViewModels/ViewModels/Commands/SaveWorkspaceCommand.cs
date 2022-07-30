using System;
using System.Windows.Input;

namespace ViewModels.ViewModels.Commands;

public class SaveWorkspaceCommand : ICommand
{
  private readonly ComponentInstancesViewModel _componentInstancesViewModel;
  private readonly PersistentModelContentBuilderFactory _persistentModelContentBuilderFactory;

  public SaveWorkspaceCommand(
    ComponentInstancesViewModel componentInstancesViewModel, 
    PersistentModelContentBuilderFactory persistentModelContentBuilderFactory)
  {
    _componentInstancesViewModel = componentInstancesViewModel;
    _persistentModelContentBuilderFactory = persistentModelContentBuilderFactory;
  }

  public bool CanExecute(object parameter)
  {
    return true;
  }

  public void Execute(object parameter)
  {
    var persistentModelFileContentBuilder = _persistentModelContentBuilderFactory.CreateInstance();
    _componentInstancesViewModel.SaveTo(persistentModelFileContentBuilder);
  }

  public event EventHandler CanExecuteChanged;

  protected virtual void OnCanExecuteChanged()
  {
    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
  }
}

public class PersistentModelContentBuilderFactory
{
  private readonly OperationsOutputViewModel _operationsOutputViewModel;
  private readonly OperationMachinesByControlObject _operationMachinesByControlObject;

  public PersistentModelContentBuilderFactory(OperationsOutputViewModel operationsOutputViewModel, OperationMachinesByControlObject operationMachinesByControlObject)
  {
    _operationsOutputViewModel = operationsOutputViewModel;
    _operationMachinesByControlObject = operationMachinesByControlObject;
  }

  public PersistentModelFileContentBuilder CreateInstance()
  {
    return new PersistentModelFileContentBuilder(_operationsOutputViewModel, _operationMachinesByControlObject);
  }
}