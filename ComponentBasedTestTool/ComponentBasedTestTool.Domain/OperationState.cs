using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain
{
  public interface OperationState
  {
    void Start(OperationContext context, Operation operation);
    void DependencyFulfilled(OperationContext operationViewModel);
  }
}