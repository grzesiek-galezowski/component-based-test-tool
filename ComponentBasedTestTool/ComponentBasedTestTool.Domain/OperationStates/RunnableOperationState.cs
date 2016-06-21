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
    public RunnableOperationState()
    {
    }

    public void Start(OperationContext context, Runnable operation)
    {
      Run(new CancellationTokenSource(), context, operation);
    }

    private static void Run(CancellationTokenSource cancellationTokenSource, OperationContext context, Runnable operation)
    {
      Task.Run(async () =>
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
      });
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