using System;
using ExtensionPoints;

namespace ViewModels.ViewModels.OperationStates
{
  public class NotExecutableOperationState : OperationState
  {
    public void Run(OperationViewModel operationViewModel, Operation operation)
    {
      throw new NotSupportedException("The operation cannot be invoked right now");
    }

    public void DependencyFulfilled(OperationViewModel operationViewModel)
    {
      operationViewModel.Ready();
    }
  }
}