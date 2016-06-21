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
    private readonly CancellationTokenSource _cancellationTokenSource;

    public RunnableOperationState(CancellationTokenSource cancellationTokenSource)
    {
      _cancellationTokenSource = cancellationTokenSource;
    }

    public void Start(OperationContext context, Runnable operation)
    {
      Run(_cancellationTokenSource, context, operation);
    }

    private static void Run(CancellationTokenSource cancellationTokenSource, OperationContext context, Runnable operation)
    {
      Task.Run(async () =>
      {
        try
        {
          context.InProgress();
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
      }).ContinueWith((t,o) =>
      {
        
      }, null);
    }

    public void DependencyFulfilled(OperationContext operationViewModel)
    {
      
    }
  }
}