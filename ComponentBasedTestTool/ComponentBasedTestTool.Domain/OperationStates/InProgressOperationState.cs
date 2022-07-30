using System.Threading;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain.OperationStates;

public class InProgressOperationState : OperationState
{
  private readonly CancellationTokenSource _cancellationTokenSource;

  public InProgressOperationState(CancellationTokenSource cancellationTokenSource)
  {
    _cancellationTokenSource = cancellationTokenSource;
  }

  public void Start(OperationContext context, Runnable operation)
  {
    // Method intentionally left empty.
  }

  public void DependencyFulfilled(OperationContext operationViewModel)
  {
    // Method intentionally left empty.
  }

  public void Stop()
  {
    _cancellationTokenSource.Cancel();
  }

  public void Notify(OperationContext context)
  {
    context.InProgress(_cancellationTokenSource);
  }
}