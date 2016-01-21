using System.Threading;
using System.Windows;

namespace ViewModels.ViewModels.Commands
{
  public class StopOperationCommand : OperationCommand
  {
    private readonly OperationStateMachine _operation;

    public StopOperationCommand(
      ApplicationContext applicationContext, 
      OperationStateMachine operation) 
      : base(false, applicationContext)
    {
      _operation = operation;
    }

    public override void Execute(object parameter)
    {
      _operation.Stop();
    }

  }
}