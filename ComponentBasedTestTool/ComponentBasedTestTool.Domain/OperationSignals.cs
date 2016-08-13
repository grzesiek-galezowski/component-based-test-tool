using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain
{
  public interface OperationSignals : OperationControl
  {
    void DependencyFulfilled(OperationContext context);
    void FromNowOnReportSuccessfulExecutionTo(OperationDependencyObserver observer);
  }
}