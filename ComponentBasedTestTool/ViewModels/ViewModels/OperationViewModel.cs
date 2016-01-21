using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using CallMeMaybe;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ViewModels.ViewModels.Commands;
using ViewModels.ViewModels.OperationStates;

namespace ViewModels.ViewModels
{
  public interface OperationExecutionObserver
  {
    void DependencyFulfilled();
  }

  public class OperationViewModel  : INotifyPropertyChanged, OperationContext, OperationExecutionObserver
  {
    private string _stateString;
    private OperationState _operationState;
    private OperationCommand _runCommand;
    private OperationCommand _stopCommand;
    private string _lastError = string.Empty;
    private string _lastErrorFullText = "lolokimono";
    private readonly Maybe<string> _maybeDependencyName;
    private readonly OperationPropertiesViewModelBuilder _propertyListBuilder;
    private object _cachedObject;
    private readonly OperationCommandFactory _operationCommandFactory;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly OperationStateMachine _operationStateMachine;

    public OperationViewModel(string name, Operation operation, Maybe<string> maybeDependencyName, OperationCommandFactory operationCommandFactory)
    {
      _cancellationTokenSource = new CancellationTokenSource();
      Name = name;
      _maybeDependencyName = maybeDependencyName;
      _propertyListBuilder = new OperationPropertiesViewModelBuilder(name);
      _operationCommandFactory = operationCommandFactory;

      operation.FillParameters(_propertyListBuilder);

      _operationStateMachine = CreateOperationStateMachine(operation);
      if (_maybeDependencyName.HasValue)
      {
        _operationStateMachine.Initial(this);
      }
      else
      {
        _operationStateMachine.Ready(this, _cancellationTokenSource);
      }
    }

    private static OperationStateMachine CreateOperationStateMachine(Operation operation)
    {
      return new OperationStateMachine(
        operation,
        new NotExecutableOperationState(),
        new List<OperationExecutionObserver>());
    }


    public OperationCommand RunOperationCommand 
      => _runCommand ?? (_runCommand = _operationCommandFactory.CreateRunCommand(this));

    public OperationCommand StopOperationCommand
      => _stopCommand ?? (_stopCommand = 
      _operationCommandFactory.CreateStopCommand(_cancellationTokenSource));


    public string Name { get; }

    public string LastError
    {
      get { return _lastError; }
      set
      {
        _lastError = value;
        OnPropertyChanged();
      }
    }

    public string LastErrorFullText
    {
      get { return _lastErrorFullText; }
      set
      {
        _lastErrorFullText = value;
        OnPropertyChanged();
      }
    }


    public string State
    {
      get { return _stateString; }
      set
      {
        _stateString = value;
        OnPropertyChanged();
      }
    }

    public bool CanRun
    {
      get { return RunOperationCommand.CanExecuteValue; }
      set { RunOperationCommand.CanExecuteValue = value; }
    }

    public bool CanStop
    {
      get { return StopOperationCommand.CanExecuteValue; }
      set { StopOperationCommand.CanExecuteValue = value; }
    }

    public void Run()
    {
      _operationStateMachine.Run(this);
    }

    public void Initial()
    {
      _operationStateMachine.Initial(this);
    }

    public void NotifyonCurrentState(
      bool canRun, 
      bool canStop, 
      string initial, 
      string lastErrorFullText, 
      string lastError)
    {
      CanRun = canRun;
      CanStop = canStop;
      State = initial;
      LastErrorFullText = lastErrorFullText;
      LastError = lastError;
    }

    public void Ready()
    {
      _operationStateMachine.Ready(this, _cancellationTokenSource);
    }

    public void Success()
    {
      _operationStateMachine.Success(this, _cancellationTokenSource);
    }

    public void DependencyFulfilled()
    {
      _operationStateMachine.DependencyFulfilled(this);
    }

    public void Stopped()
    {
      _operationStateMachine.Stopped(this, _cancellationTokenSource);
    }

    public void Failure(Exception exception)
    {
      _operationStateMachine.Failure(exception, this, _cancellationTokenSource);
    }

    public void InProgress()
    {
      _operationStateMachine.InProgress(this);
    }

    public void SetPropertiesOn(OperationPropertiesViewModel operationPropertiesViewModel)
    {
      operationPropertiesViewModel.Properties = 
        _cachedObject ?? (_cachedObject = _propertyListBuilder.Build());
    }

    #region Boilerplate
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    public void ObserveMatching(IEnumerable<OperationViewModel> operationViewModel)
    {
      if (_maybeDependencyName.HasValue)
      {
        var dependencyName = _maybeDependencyName.Single();
        var observedOperation = FindOperationWith(dependencyName, operationViewModel);
        observedOperation._operationStateMachine.FromNowOnReportSuccessfulExecutionTo(this);
      }
    }

    private static OperationViewModel FindOperationWith(string dependencyName, IEnumerable<OperationViewModel> operationViewModel)
    {
      return operationViewModel.First(o => o.IsKnownAs(dependencyName));
    }

    private bool IsKnownAs(string name) => Name == name;
  }
}

//VTODO dependent operations
//TODO persistence
//TODO script view
//TODO removing components 
//TODO what happens when we remove operation while it is in progress?
//TODO component view and script view
//TODO communication view like wireshark
//TODO in component addition, create button called "Add All Selected"
//TODO plugin mechanism
//TODO output formatting plugins
//TODO Saving  layout

