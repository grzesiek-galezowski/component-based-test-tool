using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ComponentBasedTestTool.Domain.OperationStates;
using ExtensionPoints.ImplementedByComponents;
using ViewModels;
using ViewModels.ViewModels;

namespace ComponentBasedTestTool.Domain
{
  

  public class DefaultOperationStateMachine : OperationStateMachine
  {
    private readonly List<OperationDependencyObserver> _observers;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly Operation _operation;
    private OperationState _operationState;

    public DefaultOperationStateMachine(
      Operation operation, 
      OperationState operationState, 
      List<OperationDependencyObserver> observers, 
      CancellationTokenSource cancellationTokenSource)
    {
      _operation = operation;
      _operationState = operationState;
      _observers = observers;
      _cancellationTokenSource = cancellationTokenSource;
    }

    public void DependencyFulfilled(OperationContext operationViewModel)
    {
      _operationState.DependencyFulfilled(operationViewModel);
    }

    public void Failure(Exception exception, OperationContext operationViewModel)
    {
      var lastError = Format(exception);
      operationViewModel.NotifyonCurrentState(true, false, "Failure", exception.ToString(), lastError);

      _operationState = ExecutableState(_cancellationTokenSource);
    }

    public void FromNowOnReportSuccessfulExecutionTo(OperationDependencyObserver observer)
    {
      _observers.Add(observer);
    }

    public void Initial(OperationContext observer)
    {
      observer.NotifyonCurrentState(
        false,
        false,
        "Initial",
        string.Empty,
        string.Empty);

      _operationState = new NotExecutableOperationState();
    }

    public void InProgress(OperationContext operationViewModel)
    {
      operationViewModel.NotifyonCurrentState(
        false,
        true,
        "In Progress",
        string.Empty,
        string.Empty);

      _operationState = new InProgressOperationState();
    }

    public void Ready(OperationContext context)
    {
      NormalExecutable(context, "Ready", _cancellationTokenSource);
    }

    public void Start(OperationContext context)
    {
      _operationState.Start(context, _operation);
    }

    public void Stopped(OperationContext operationViewModel)
    {
      NormalExecutable(operationViewModel, "Stopped", _cancellationTokenSource);
    }

    public void Success(OperationContext operationViewModel)
    {
      NormalExecutable(operationViewModel, "Success", _cancellationTokenSource);
      foreach (var observer in _observers)
      {
        observer.DependencyFulfilled();
      }
    }

    public void Stop()
    {
      _cancellationTokenSource.Cancel();
    }

    private static ExecutableOperationState ExecutableState(CancellationTokenSource cancellationTokenSource)
    {
      return new ExecutableOperationState(cancellationTokenSource);
    }

    private static string Format(Exception exception)
    {
      return exception.ToString().Split(
        new[] { Environment.NewLine },
        StringSplitOptions.RemoveEmptyEntries).First();
    }

    private void NormalExecutable(OperationContext context, string statusText, CancellationTokenSource cancellationTokenSource)
    {
      context.NotifyonCurrentState(
        true,
        false,
        statusText,
        string.Empty,
        string.Empty);
      _operationState = ExecutableState(cancellationTokenSource);
    }
  }


}