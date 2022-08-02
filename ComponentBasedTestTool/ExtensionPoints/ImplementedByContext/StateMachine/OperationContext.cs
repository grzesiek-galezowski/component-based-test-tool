using System;
using System.Threading;

namespace ExtensionPoints.ImplementedByContext.StateMachine;

public interface IOperationContext
{
  void Ready();
  void Success();
  void Stopped();
  void Failure(Exception exception);
  void InProgress(CancellationTokenSource cancellationTokenSource);
  void Initial();

  void NotifyOnCurrentState(string stateName, Runnability runnability, ErrorInfo errorInfo);
}