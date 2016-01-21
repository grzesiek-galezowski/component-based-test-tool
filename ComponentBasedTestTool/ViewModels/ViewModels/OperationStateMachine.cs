using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ViewModels.ViewModels.OperationStates;

namespace ViewModels.ViewModels
{
  public class OperationStateMachine
  {
    private readonly List<OperationExecutionObserver> _observers;
    private readonly Operation _operation;
    private OperationState _operationState;

    public OperationStateMachine(
      Operation operation,
      OperationState operationState,
      List<OperationExecutionObserver> observers)
    {
      _operation = operation;
      _operationState = operationState;
      _observers = observers;
    }

    public void DependencyFulfilled(OperationContext operationViewModel)
    {
      _operationState.DependencyFulfilled(operationViewModel);
    }

    public void Failure(Exception exception, OperationContext operationViewModel, CancellationTokenSource cancellationTokenSource)
    {
      var lastError = Format(exception);
      operationViewModel.NotifyonCurrentState(true, false, "Failure", exception.ToString(), lastError);

      _operationState = ExecutableState(cancellationTokenSource);
    }

    public void FromNowOnReportSuccessfulExecutionTo(OperationExecutionObserver observer)
    {
      _observers.Add(observer);
    }

    public void Initial(OperationContext observer)
    {
      observer.NotifyonCurrentState(
        false,
        false,
        "Initial",
        String.Empty,
        String.Empty);

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

    public void Ready(OperationContext context, CancellationTokenSource cancellationTokenSource)
    {
      NormalExecutable(context, "Ready", cancellationTokenSource);
    }

    public void Run(OperationContext context) //bug
    {
      _operationState.Run(context, _operation);
    }

    public void Stopped(OperationContext operationViewModel, CancellationTokenSource cancellationTokenSource)
    {
      NormalExecutable(operationViewModel, "Stopped", cancellationTokenSource);
    }

    public void Success(OperationContext operationViewModel, CancellationTokenSource cancellationTokenSource)
    {
      NormalExecutable(operationViewModel, "Success", cancellationTokenSource);
      foreach (var observer in _observers)
      {
        observer.DependencyFulfilled();
      }
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