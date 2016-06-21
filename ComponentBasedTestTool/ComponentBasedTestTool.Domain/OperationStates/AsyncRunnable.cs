using System;
using System.Threading;
using System.Threading.Tasks;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain.OperationStates
{
  internal class AsyncRunnable
  {
    private readonly CancellationTokenSource _cancellationTokenSource;

    public AsyncRunnable(CancellationTokenSource cancellationTokenSource)
    {
      _cancellationTokenSource = cancellationTokenSource;
    }

    public void Run(CancellationTokenSource cancellationTokenSource, OperationContext context, Runnable operation)
    {
      Task.Run(async () =>
      {
        try
        {
          context.InProgress();
          await operation.RunAsync(_cancellationTokenSource.Token).ConfigureAwait(false);
          context.Success();
        }
        catch (OperationCanceledException ex)
        {
          context.Stopped();
        }
        catch (Exception e)
        {
          context.Failure(e);
        }
      });
    }
  }
}