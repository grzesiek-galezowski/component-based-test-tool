using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain
{
  public interface OperationStateMachine : OperationSignals, OperationStateObserver
  {
  }

  public class DefaultOperationStateMachine : OperationStateMachine
  {
    private readonly List<OperationDependencyObserver> _observers;
    private readonly Runnable _operation;
    private OperationState _operationState;
    private readonly OperationStatesFactory _operationStatesFactory;

    public DefaultOperationStateMachine(
      Runnable operation, 
      OperationState operationState, 
      OperationStatesFactory operationStatesFactory)
    {
      _operation = operation;
      _operationState = operationState;
      _observers = new List<OperationDependencyObserver>();
      _operationStatesFactory = operationStatesFactory;
    }

    void OperationSignals.DependencyFulfilled(OperationContext context)
    {
      _operationState.DependencyFulfilled(context);
    }

    void OperationStateObserver.Failure(Exception exception, OperationContext context)
    {
      context.NotifyonCurrentState("Failure", Runnability.Runnable(), ErrorInfo.From(exception));

      _operationState = _operationStatesFactory.RunnableState();
    }

    public void FromNowOnReportSuccessfulExecutionTo(OperationDependencyObserver observer)
    {
      _observers.Add(observer);
    }

    void OperationStateObserver.Initial(OperationContext context)
    {
      context.NotifyonCurrentState(
        "Initial", 
        Runnability.Unavailable(), 
        ErrorInfo.None());

      _operationState = _operationStatesFactory.Unavailable();
    }

    void OperationStateObserver.InProgress(OperationContext context, CancellationTokenSource cancellationTokenSource)
    {
      context.NotifyonCurrentState(
        "In Progress", 
        Runnability.InProgress(), 
        ErrorInfo.None());

      _operationState = _operationStatesFactory.InProgress(cancellationTokenSource);
    }
    void OperationControl.Start(OperationContext context)
    {
      _operationState.Start(context, _operation);
    }

    void OperationStateObserver.Ready(OperationContext context)
    {
      NormalRunnable(context, "Ready");
    }


    void OperationStateObserver.Stopped(OperationContext context)
    {
      NormalRunnable(context, "Stopped");
    }

    void OperationStateObserver.Success(OperationContext context)
    {
      NormalRunnable(context, "Success");
      foreach (var observer in _observers)
      {
        observer.DependencyFulfilled();
      }
    }

    void OperationControl.Stop()
    {
      _operationState.Stop();
    }

    private void NormalRunnable(OperationContext context, string statusText)
    {
      context.NotifyonCurrentState(statusText, Runnability.Runnable(), ErrorInfo.None());
      _operationState = _operationStatesFactory.RunnableState();
    }

    public static DefaultOperationStateMachine StateMachineFor(Runnable componentOperation, BackgroundTasks backgroundTasks)
    {
      return new DefaultOperationStateMachine(
        componentOperation,
        new UnavailableOperationState(), new OperationStatesFactory(backgroundTasks));
    }
  }


}