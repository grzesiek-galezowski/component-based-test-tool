using System;
using System.Collections.Generic;
using System.Threading;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain;

public class DefaultOperationStateMachine : IOperationStateMachine
{
  private readonly List<IOperationDependencyObserver> _observers;
  private readonly IComponentOperation _operation;
  private IOperationState _operationState;
  private readonly OperationStatesFactory _operationStatesFactory;
  private readonly BroadcastContext _context = new();

  public DefaultOperationStateMachine(
    IComponentOperation operation, 
    IOperationState operationState, 
    OperationStatesFactory operationStatesFactory)
  {
    _operation = operation;
    _operationState = operationState;
    _observers = new List<IOperationDependencyObserver>();
    _operationStatesFactory = operationStatesFactory;
  }

  void IOperationSignals.DependencyFulfilled(IOperationContext context)
  {
    _operationState.DependencyFulfilled(context);
  }

  void IOperationStateObserver.Failure(Exception exception, IOperationContext context)
  {
    context.NotifyOnCurrentState("Failure", Runnability.Runnable(), ErrorInfo.From(exception));

    _operationState = _operationStatesFactory.RunnableState();
  }

  public void FromNowOnReportSuccessfulExecutionTo(IOperationDependencyObserver observer)
  {
    _observers.Add(observer);
  }

  void IOperationStateObserver.Initial(IOperationContext context)
  {
    context.NotifyOnCurrentState(
      "Initial", 
      Runnability.Unavailable(), 
      ErrorInfo.None());

    _operationState = _operationStatesFactory.Unavailable();
  }

  void IOperationStateObserver.InProgress(IOperationContext context, CancellationTokenSource cancellationTokenSource)
  {
    context.NotifyOnCurrentState(
      "In Progress", 
      Runnability.InProgress(), 
      ErrorInfo.None());

    _operationState = _operationStatesFactory.InProgress(cancellationTokenSource);
  }

  public void InitializeParametersIn(IOperationParametersListBuilder operationParametersListBuilder)
  {
    _operation.InitializeParameters(operationParametersListBuilder);
  }

  public void SaveUsing(IPersistentStorage persistentStorage, string name, IConfigurationOutputBuilder builder)
  {
    builder.AppendOperationNode(name, _operation);
    _operation.StoreParameters(persistentStorage);
  }

  public void RegisterContext(IOperationContext context)
  {
    _context.Register(context);
    _operationState.Notify(context);
  }

  void IOperationControl.Start()
  {
    _operationState.Start(_context, _operation);
  }

  void IOperationStateObserver.Ready(IOperationContext context)
  {
    NormalRunnable(context, "Ready");
  }


  void IOperationStateObserver.Stopped(IOperationContext context)
  {
    NormalRunnable(context, "Stopped");
  }

  void IOperationStateObserver.Success(IOperationContext context)
  {
    NormalRunnable(context, "Success");
    foreach (var observer in _observers)
    {
      observer.DependencyFulfilled();
    }
  }

  void IOperationControl.Stop()
  {
    _operationState.Stop();
  }

  public void DeregisterContext(IOperationContext context)
  {
    _context.Deregister(context);
  }

  private void NormalRunnable(IOperationContext context, string statusText)
  {
    context.NotifyOnCurrentState(statusText, Runnability.Runnable(), ErrorInfo.None());
    _operationState = _operationStatesFactory.RunnableState();
  }

}

public class BroadcastContext : IOperationContext
{
  private readonly List<IOperationContext> _contexts;

  public BroadcastContext(params IOperationContext[] context)
  {
    _contexts = new List<IOperationContext>(context);
  }

  public void Register(IOperationContext context)
  {
    _contexts.Add(context);
  }

  public void Ready()
  {
    foreach (var operationContext in _contexts)
    {
      operationContext.Ready();
    }
  }

  public void Success()
  {
    foreach (var operationContext in _contexts)
    {
      operationContext.Success();
    }
  }

  public void Stopped()
  {
    foreach (var operationContext in _contexts)
    {
      operationContext.Stopped();
    }
  }

  public void Failure(Exception exception)
  {
    foreach (var operationContext in _contexts)
    {
      operationContext.Failure(exception);
    }
  }

  public void InProgress(CancellationTokenSource cancellationTokenSource)
  {
    foreach (var operationContext in _contexts)
    {
      operationContext.InProgress(cancellationTokenSource);
    }
  }

  public void Initial()
  {
    foreach (var operationContext in _contexts)
    {
      operationContext.Initial();
    }
  }

  public void NotifyOnCurrentState(string stateName, Runnability runnability, ErrorInfo errorInfo)
  {
    foreach (var operationContext in _contexts)
    {
      operationContext.NotifyOnCurrentState(stateName, runnability, errorInfo);
    }
  }

  public void Deregister(IOperationContext context)
  {
    _contexts.Remove(context);
  }
}