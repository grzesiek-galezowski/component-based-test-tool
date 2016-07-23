using System;
using System.Threading;

namespace ExtensionPoints.ImplementedByContext.StateMachine
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
}