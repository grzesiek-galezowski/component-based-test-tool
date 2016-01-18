using System;
using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;

namespace ViewModels.ViewModels.OperationStates
{
  public sealed class ExecutableOperationState : OperationState
  {
    private readonly CancellationTokenSource _cancellationTokenSource;

    public ExecutableOperationState(CancellationTokenSource cancellationTokenSource)
    {
      _cancellationTokenSource = cancellationTokenSource;
    }

    public void Run(OperationViewModel operationViewModel, Operation operation)
    {
      Task.Run(async () =>
      {
        try
        {
          operationViewModel.InProgress();
          await operation.RunAsync(_cancellationTokenSource.Token).ConfigureAwait(false);
          operationViewModel.Success();
        }
        catch (OperationCanceledException ex)
        {
          operationViewModel.Stopped();
        }
        catch (Exception e)
        {
          operationViewModel.Failure(e);
        }
      });
    }

    public void DependencyFulfilled(OperationViewModel operationViewModel)
    {
      
    }
  }
}