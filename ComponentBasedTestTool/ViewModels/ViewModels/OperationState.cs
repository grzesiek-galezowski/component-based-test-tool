using ExtensionPoints;

namespace ViewModels.ViewModels
{
  public interface OperationState
  {
    void Run(OperationViewModel operationViewModel, Operation operation);
  }
}