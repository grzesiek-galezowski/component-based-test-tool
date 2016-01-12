using System.Windows;

namespace ViewModels.ViewModels.Commands
{
  public class CancelOperationCommand : OperationCommand
  {
    private int _executionCount = 0;

    public CancelOperationCommand(
      ApplicationContext applicationContext) 
      : base(false, applicationContext)
    {
    }

    public override void Execute(object parameter)
    {
      _executionCount++;
      if (_executionCount % 2 == 0)
      {
        _canExecute = false;
        OnCanExecuteChanged();
      }
    }

  }
}