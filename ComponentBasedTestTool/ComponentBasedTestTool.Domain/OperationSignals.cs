using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain;

public interface IOperationSignals : IOperationControl
{
  void DependencyFulfilled(IOperationContext context);
  void FromNowOnReportSuccessfulExecutionTo(IOperationDependencyObserver observer);
}