using System;
using System.Threading;
using System.Threading.Tasks;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain.OperationStates
{
  public sealed class ExecutableOperationState : OperationState
  {
    private readonly CancellationTokenSource _cancellationTokenSource;

    public ExecutableOperationState(CancellationTokenSource cancellationTokenSource)
    {
      _cancellationTokenSource = cancellationTokenSource;
    }

    public void Start(OperationContext context, Operation operation)
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

    public void DependencyFulfilled(OperationContext operationViewModel)
    {
      
    }
  }
}