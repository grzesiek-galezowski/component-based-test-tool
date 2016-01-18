using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;

namespace ViewModels.ViewModels
{
  public interface OperationState
  {
    void Run(OperationViewModel operationViewModel, Operation operation);
    void DependencyFulfilled(OperationViewModel operationViewModel);
  }
}