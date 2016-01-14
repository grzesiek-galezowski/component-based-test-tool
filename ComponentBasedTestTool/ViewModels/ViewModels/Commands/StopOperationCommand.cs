using System.Threading;
using System.Windows;

namespace ViewModels.ViewModels.Commands
{
  public class StopOperationCommand : OperationCommand
  {
    private readonly CancellationTokenSource _cancellationTokenSource;

    public StopOperationCommand(
      ApplicationContext applicationContext, 
      CancellationTokenSource cancellationTokenSource) 
      : base(false, applicationContext)
    {
      _cancellationTokenSource = cancellationTokenSource;
    }

    public override void Execute(object parameter)
    {
      _cancellationTokenSource.Cancel();
    }

  }
}