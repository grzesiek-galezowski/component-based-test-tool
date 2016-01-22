using System;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain.OperationStates
{
  public class NotExecutableOperationState : OperationState
  {
    public void Start(OperationContext context, Operation operation)
    {
      throw new NotSupportedException("The operation cannot be invoked right now");
    }

    public void DependencyFulfilled(OperationContext operationViewModel)
    {
      operationViewModel.Ready();
    }
  }
}