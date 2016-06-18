using System;

namespace ComponentBasedTestTool.ViewModels.Ports
{
  public interface OperationStateMachine
  {
    void DependencyFulfilled(OperationContext context);
    void Failure(Exception exception, OperationContext context);
    void FromNowOnReportSuccessfulExecutionTo(OperationDependencyObserver observer);
    void Initial(OperationContext context);
    void InProgress(OperationContext context);
    void Ready(OperationContext context);
    void Start(OperationContext context);
    void Stopped(OperationContext context);
    void Success(OperationContext context);
    void Stop();
  }
}
