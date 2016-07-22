using System;
using System.Windows.Input;

namespace ViewModels.ViewModels.Commands
{
  public class SaveWorkspaceCommand : ICommand
  {
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;
    private readonly OperationsOutputViewModel _operationsOutputViewModel;

    public SaveWorkspaceCommand(
      ComponentInstancesViewModel componentInstancesViewModel, 
      OperationsOutputViewModel operationsOutputViewModel)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _operationsOutputViewModel = operationsOutputViewModel;
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      _componentInstancesViewModel.SaveTo(new FileBasedPersistentStorage(_operationsOutputViewModel));
    }

    public event EventHandler CanExecuteChanged;

    protected virtual void OnCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}