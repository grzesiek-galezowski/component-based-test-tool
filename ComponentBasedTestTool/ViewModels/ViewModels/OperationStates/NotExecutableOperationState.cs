using System;
using System.Threading.Tasks;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ViewModels.ViewModels.OperationStates
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