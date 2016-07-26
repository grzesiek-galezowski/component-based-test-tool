using System;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain.OperationStates
{
  public class UnavailableOperationState : OperationState
  {
    public void Start(OperationContext context, Runnable operation)
    {
      throw new NotSupportedException("The operation cannot be invoked right now");
    }

    public void DependencyFulfilled(OperationContext operationViewModel)
    {
      operationViewModel.Ready();
    }

    public void Stop()
    {
      throw new NotImplementedException();
    }

    public void Notify(OperationContext context)
    {
      context.Initial();
    }
  }
}