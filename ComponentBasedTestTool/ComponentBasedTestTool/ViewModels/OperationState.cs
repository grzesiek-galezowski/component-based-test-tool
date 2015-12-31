using ComponentBasedTestTool.ViewModels.OperationStates;

namespace ComponentBasedTestTool.ViewModels
{
  public interface OperationState
  {
    void Run(OperationViewModel operationViewModel, Operation operation);
  }
}