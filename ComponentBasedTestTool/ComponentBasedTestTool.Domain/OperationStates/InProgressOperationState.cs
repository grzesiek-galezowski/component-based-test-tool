using ExtensionPoints.ImplementedByComponents;
using ViewModels;
using ViewModels.ViewModels;

namespace ComponentBasedTestTool.Domain.OperationStates
{
  public class InProgressOperationState : OperationState
  {
    public void Start(OperationContext context, Operation operation)
    {
      
    }

    public void DependencyFulfilled(OperationContext operationViewModel)
    {
      
    }
  }
}