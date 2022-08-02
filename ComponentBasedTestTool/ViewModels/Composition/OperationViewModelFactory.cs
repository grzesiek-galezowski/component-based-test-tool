using ComponentBasedTestTool.Domain;
using ViewModels.ViewModels;

namespace ViewModels.Composition;

public interface IOperationViewModelFactory
{
  OperationViewModel CreateOperationViewModel(OperationEntry operationEntry, IOperationStateMachine operationStateMachine);
}