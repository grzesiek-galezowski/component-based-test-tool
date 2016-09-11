using System;
using System.Windows;
using System.Windows.Input;

namespace ViewModels.ViewModels
{
  public class OperationViewsViewModel
  {
    public ICommand ViewChangedCommand => new ViewChangedCommand();
  }

  public class ViewChangedCommand : ICommand
  {
    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      MessageBox.Show("lol");
    }

    public event EventHandler CanExecuteChanged;

    protected virtual void OnCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}