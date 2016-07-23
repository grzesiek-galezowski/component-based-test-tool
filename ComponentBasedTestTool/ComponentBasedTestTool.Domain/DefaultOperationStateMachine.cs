using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain
{

  public class DefaultOperationStateMachine : OperationStateMachine
  {
    private readonly List<OperationDependencyObserver> _observers;
    private readonly ComponentOperation _operation;
    private OperationState _operationState;
    private readonly OperationStatesFactory _operationStatesFactory;
    private OperationContext _context = new NullContext();

    public DefaultOperationStateMachine(
      ComponentOperation operation, 
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
      context.NotifyOnCurrentState("Failure", Runnability.Runnable(), ErrorInfo.From(exception));

      _operationState = _operationStatesFactory.RunnableState();
    }

    public void FromNowOnReportSuccessfulExecutionTo(OperationDependencyObserver observer)
    {
      _observers.Add(observer);
    }

    void OperationStateObserver.Initial(OperationContext context)
    {
      context.NotifyOnCurrentState(
        "Initial", 
        Runnability.Unavailable(), 
        ErrorInfo.None());

      _operationState = _operationStatesFactory.Unavailable();
    }

    void OperationStateObserver.InProgress(OperationContext context, CancellationTokenSource cancellationTokenSource)
    {
      context.NotifyOnCurrentState(
        "In Progress", 
        Runnability.InProgress(), 
        ErrorInfo.None());

      _operationState = _operationStatesFactory.InProgress(cancellationTokenSource);
    }

    public void InitializeParameters(OperationParametersListBuilder operationParametersListBuilder)
    {
      _operation.InitializeParameters(operationParametersListBuilder);
    }

    public void SaveUsing(PersistentStorage persistentStorage, string name, ConfigurationOutputBuilder builder)
    {
      builder.AppendOperationNode(name, _operation);
      _operation.StoreParameters(persistentStorage);
    }

    public void SetContext(OperationContext context)
    {
      _context = context;
    }

    void OperationControl.Start()
    {
      _operationState.Start(_context, _operation);
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
      context.NotifyOnCurrentState(statusText, Runnability.Runnable(), ErrorInfo.None());
      _operationState = _operationStatesFactory.RunnableState();
    }

  }

  internal class NullContext : OperationContext
  {
    public void Ready()
    {
      
    }

    public void Success()
    {
      
    }

    public void Stopped()
    {
    }

    public void Failure(Exception exception)
    {
    }

    public void InProgress(CancellationTokenSource cancellationTokenSource)
    {
    }

    public void Initial()
    {
    }

    public void NotifyOnCurrentState(string stateName, Runnability runnability, ErrorInfo errorInfo)
    {
    }
  }
}