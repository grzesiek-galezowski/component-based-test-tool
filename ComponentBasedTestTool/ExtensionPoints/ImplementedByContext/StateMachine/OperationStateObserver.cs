using System;
using System.Threading;

namespace ExtensionPoints.ImplementedByContext.StateMachine;

public interface IOperationStateObserver
{
  void Ready(IOperationContext context);
  void Stopped(IOperationContext context);
  void Success(IOperationContext context);
  void Failure(Exception exception, IOperationContext context);
  void Initial(IOperationContext context);
  void InProgress(IOperationContext context, CancellationTokenSource cancellationTokenSource);
}