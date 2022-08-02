using System;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain.OperationStates;

public class UnavailableOperationState : IOperationState
{
  public void Start(IOperationContext context, IRunnable operation)
  {
    throw new NotSupportedException("The operation cannot be invoked right now");
  }

  public void DependencyFulfilled(IOperationContext operationViewModel)
  {
    operationViewModel.Ready();
  }

  public void Stop()
  {
    throw new NotImplementedException();
  }

  public void Notify(IOperationContext context)
  {
    context.Initial();
  }
}