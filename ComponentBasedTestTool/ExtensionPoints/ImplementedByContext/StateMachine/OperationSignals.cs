namespace ExtensionPoints.ImplementedByContext.StateMachine
{
  public interface OperationSignals : OperationControl
  {
    void DependencyFulfilled(OperationContext context);
    void FromNowOnReportSuccessfulExecutionTo(OperationDependencyObserver observer);
  }
}