using System;
using System.Windows;

namespace ComponentBasedTestTool.ViewModels.Commands
{
  public class CancelOperationCommand : OperationCommand
  {
    private int _executionCount = 0;

    public CancelOperationCommand() : base(false)
    {
    }

    public override void Execute(object parameter)
    {
      _executionCount++;
      MessageBox.Show("Stopped command");
      if (_executionCount % 2 == 0)
      {
        _canExecute = false;
        OnCanExecuteChanged();
      }
    }

  }
}