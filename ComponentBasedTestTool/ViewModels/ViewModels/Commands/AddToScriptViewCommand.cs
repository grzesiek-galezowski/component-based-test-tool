using System;
using System.Windows.Input;

namespace ViewModels.ViewModels.Commands
{
  public class AddToScriptViewCommand : ICommand
  {
    private readonly ScriptOperationsViewModel _scriptOperationsViewModel;
    private readonly OperationViewModel _operationViewModel;

    public AddToScriptViewCommand(ScriptOperationsViewModel scriptOperationsViewModel, OperationViewModel operationViewModel)
    {
      _scriptOperationsViewModel = scriptOperationsViewModel;
      _operationViewModel = operationViewModel;
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      _scriptOperationsViewModel.Operations.Add(_operationViewModel);
    }

    public event EventHandler CanExecuteChanged;

    protected virtual void OnCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}