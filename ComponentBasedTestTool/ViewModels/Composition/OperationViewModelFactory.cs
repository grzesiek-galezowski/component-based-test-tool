using ComponentBasedTestTool.Domain;
using ViewModels.ViewModels;

namespace ViewModels.Composition;

public interface OperationViewModelFactory
{
  OperationViewModel CreateOperationViewModel(OperationEntry operationEntry, OperationStateMachine operationStateMachine);
}