using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain
{
  public interface OperationState
  {
    void Start(OperationContext context, Runnable operation);
    void DependencyFulfilled(OperationContext operationViewModel);
    void Stop();
  }
}