using System;
using System.Windows;
using System.Windows.Input;

namespace ComponentBasedTestTool.ViewModels.Commands
{
  public sealed class AddComponentCommand : ICommand
  {
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      MessageBox.Show("OK!");
    }

    private void OnCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}