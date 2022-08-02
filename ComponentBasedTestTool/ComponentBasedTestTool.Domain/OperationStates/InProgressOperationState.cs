using System.Threading;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain.OperationStates;

public class InProgressOperationState : IOperationState
{
  private readonly CancellationTokenSource _cancellationTokenSource;

  public InProgressOperationState(CancellationTokenSource cancellationTokenSource)
  {
    _cancellationTokenSource = cancellationTokenSource;
  }

  public void Start(IOperationContext context, IRunnable operation)
  {
    // Method intentionally left empty.
  }

  public void DependencyFulfilled(IOperationContext operationViewModel)
  {
    // Method intentionally left empty.
  }

  public void Stop()
  {
    _cancellationTokenSource.Cancel();
  }

  public void Notify(IOperationContext context)
  {
    context.InProgress(_cancellationTokenSource);
  }
}