using System;
using System.Threading;
using ComponentBasedTestTool.ViewModels.Ports;

namespace ExtensionPoints.ImplementedByContext
{
  public interface OperationStateObserver
  {
    void Ready(OperationContext context);
    void Stopped(OperationContext context);
    void Success(OperationContext context);
    void Failure(Exception exception, OperationContext context);
    void Initial(OperationContext context);
    void InProgress(OperationContext context, CancellationTokenSource cancellationTokenSource);
  }

  public interface OperationControl
  {
    void Start(OperationContext context);
    void Stop();
  }

  public interface OperationSignals : OperationControl
  {
    void DependencyFulfilled(OperationContext context);
    void FromNowOnReportSuccessfulExecutionTo(OperationDependencyObserver observer);
  }

  public interface OperationStateMachine : OperationSignals, OperationStateObserver
  {
    void InitializeParameters(OperationParametersListBuilder operationParametersListBuilder);
    void SaveUsing(PersistentStorage persistentStorage, string name);
  }

}
