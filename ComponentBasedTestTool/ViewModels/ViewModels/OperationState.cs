using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ViewModels.ViewModels
{
  public interface OperationState
  {
    void Run(OperationContext context, Operation operation);
    void DependencyFulfilled(OperationContext operationViewModel);
  }
}