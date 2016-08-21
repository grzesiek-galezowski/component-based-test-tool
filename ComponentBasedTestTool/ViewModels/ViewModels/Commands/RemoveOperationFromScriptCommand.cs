using System;
using System.Windows.Input;

namespace ViewModels.ViewModels.Commands
{
  public class RemoveOperationFromScriptCommand : ICommand
  {
    private readonly OperationViewModel _operationViewModel;
    private readonly OperationsViewModel _scriptOperationsViewModel;

    public RemoveOperationFromScriptCommand(OperationViewModel operationViewModel, OperationsViewModel scriptOperationsViewModel)
    {
      _operationViewModel = operationViewModel;
      _scriptOperationsViewModel = scriptOperationsViewModel;
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      _scriptOperationsViewModel.Operations.Remove(_operationViewModel);
    }

    public event EventHandler CanExecuteChanged;

    protected virtual void OnCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}