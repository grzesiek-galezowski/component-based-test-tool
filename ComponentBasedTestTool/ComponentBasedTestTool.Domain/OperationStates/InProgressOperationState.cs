using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain.OperationStates
{
  public class InProgressOperationState : OperationState
  {
    public void Start(OperationContext context, Operation operation)
    {
      // Method intentionally left empty.
    }

    public void DependencyFulfilled(OperationContext operationViewModel)
    {
      // Method intentionally left empty.
    }
  }
}