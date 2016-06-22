using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain.OperationStates
{
  public sealed class RunnableOperationState : OperationState
  {
    private readonly BackgroundTasks _backgroundTasks;

    public RunnableOperationState(BackgroundTasks backgroundTasks)
    {
      _backgroundTasks = backgroundTasks;
    }

    public void Start(OperationContext context, Runnable operation)
    {
      _backgroundTasks.Launch(PerformRun, context, operation);
    }

    private static async Task PerformRun(CancellationTokenSource cancellationTokenSource, OperationContext context,
      Runnable operation)
    {
      try
      {
        context.InProgress(cancellationTokenSource);
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
      finally
      {
        cancellationTokenSource.Dispose();
      }
    }

    public void DependencyFulfilled(OperationContext operationViewModel)
    {
      
    }

    public void Stop()
    {
      throw new NotImplementedException();
    }
  }
}