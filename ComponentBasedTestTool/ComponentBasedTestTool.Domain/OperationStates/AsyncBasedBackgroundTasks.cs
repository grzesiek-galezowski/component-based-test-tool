using System;
using System.Threading;
using System.Threading.Tasks;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain.OperationStates;

public class AsyncBasedBackgroundTasks : BackgroundTasks
{
  public void Launch<T2, T3>(Func<CancellationTokenSource, T2, T3, Task> func, T2 arg2, T3 arg3)
  {
    var cancellationTokenSource = new CancellationTokenSource();
    Task.Run(async () =>
      {
        await func(cancellationTokenSource, arg2, arg3).ConfigureAwait(false);
      })
      .ContinueWith((t,o) => cancellationTokenSource.Dispose(), null);
  }

  public static async Task PerformRun(CancellationTokenSource cancellationTokenSource, OperationContext context,
    Runnable operation)
  {
    context.InProgress(cancellationTokenSource);
    try
    {
      await operation.RunAsync(cancellationTokenSource.Token).ConfigureAwait(false);
      context.Success();
    }
    catch (OperationCanceledException)
    {
      context.Stopped();
    }
    catch (Exception e)
    {
      context.Failure(e);
    }
  }

  public void Run(Runnable operation, OperationContext context)
  {
    Launch(AsyncBasedBackgroundTasks.PerformRun, context, operation);
  }
}