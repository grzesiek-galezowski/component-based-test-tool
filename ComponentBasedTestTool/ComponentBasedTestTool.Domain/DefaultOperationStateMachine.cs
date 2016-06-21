using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain
{
  

  public class DefaultOperationStateMachine : OperationStateMachine
  {
    private readonly List<OperationDependencyObserver> _observers;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly Runnable _operation;
    private OperationState _operationState;
    private readonly OperationStatesFactory _operationStatesFactory;

    public DefaultOperationStateMachine(
      Runnable operation, 
      OperationState operationState, 
      CancellationTokenSource cancellationTokenSource, 
      OperationStatesFactory operationStatesFactory)
    {
      _operation = operation;
      _operationState = operationState;
      _observers = new List<OperationDependencyObserver>();
      _cancellationTokenSource = cancellationTokenSource;
      _operationStatesFactory = operationStatesFactory;
    }

    public void DependencyFulfilled(OperationContext context)
    {
      _operationState.DependencyFulfilled(context);
    }

    public void Failure(Exception exception, OperationContext context)
    {
      context.NotifyonCurrentState(nameof(Failure), Runnability.Runnable(), ErrorInfo.From(exception));

      _operationState = _operationStatesFactory.RunnableState();
    }

    public void FromNowOnReportSuccessfulExecutionTo(OperationDependencyObserver observer)
    {
      _observers.Add(observer);
    }

    public void Initial(OperationContext context)
    {
      context.NotifyonCurrentState(
        nameof(Initial), 
        Runnability.Unavailable(), 
        ErrorInfo.None());

      _operationState = _operationStatesFactory.Unavailable();
    }

    public void InProgress(OperationContext context)
    {
      context.NotifyonCurrentState(
        "In Progress", 
        Runnability.InProgress(), 
        ErrorInfo.None());

      _operationState = _operationStatesFactory.InProgress();
    }

    public void Ready(OperationContext context)
    {
      NormalRunnable(context, nameof(Ready));
    }

    public void Start(OperationContext context)
    {
      _operationState.Start(context, _operation);
    }

    public void Stopped(OperationContext context)
    {
      NormalRunnable(context, nameof(Stopped));
    }

    public void Success(OperationContext context)
    {
      NormalRunnable(context, nameof(Success));
      foreach (var observer in _observers)
      {
        observer.DependencyFulfilled();
      }
    }

    public void Stop()
    {
      _cancellationTokenSource.Cancel();
    }

    private void NormalRunnable(OperationContext context, string statusText)
    {
      context.NotifyonCurrentState(statusText, Runnability.Runnable(), ErrorInfo.None());
      _operationState = _operationStatesFactory.RunnableState();
    }
  }


}