﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.Domain;
using Core.Maybe;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
using ViewModels.Composition;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels;

public class OperationViewModel  : INotifyPropertyChanged, IOperationContext, IOperationDependencyObserver
{
  private string? _stateString;
  private OperationCommand? _runCommand;
  private OperationCommand? _stopCommand;
  private RestartOperationCommand? _restartCommand;
  private AddToScriptViewCommand? _addToScriptViewCommand;
  private string _lastError = string.Empty;
  private string _lastErrorFullText = "lolokimono";
  private readonly Maybe<string> _maybeDependencyName;
  private readonly OperationPropertiesViewModelBuilder _propertyListBuilder;
  private readonly OperationCommandFactory _operationCommandFactory;
  private readonly IOperationStateMachine _operationStateMachine;
  private ICommand? _removeOperationFromScriptCommand;

  public OperationViewModel(string name, Maybe<string> maybeDependencyName, string operationEntryParentComponentInstanceName, OperationCommandFactory operationCommandFactory, OperationPropertiesViewModelBuilder operationPropertiesViewModelBuilder, IOperationStateMachine operationStateMachine)
  {
    Name = name;
    ComponentInstanceName = operationEntryParentComponentInstanceName;
    _propertyListBuilder = operationPropertiesViewModelBuilder;
    _maybeDependencyName = maybeDependencyName;
    _operationCommandFactory = operationCommandFactory;
    _operationStateMachine = operationStateMachine;

    if (_maybeDependencyName.HasValue)
    {
      _operationStateMachine.Initial(this);
    }
    else
    {
      _operationStateMachine.Ready(this);
    }
  }


  [UsedImplicitly]
  public OperationCommand RunOperationCommand 
    => _runCommand ??= _operationCommandFactory.CreateRunCommand(this);

  [UsedImplicitly]
  public OperationCommand StopOperationCommand
    => _stopCommand ??= _operationCommandFactory.CreateStopCommand(_operationStateMachine);

  [UsedImplicitly]
  public RestartOperationCommand RestartOperationCommand
    => _restartCommand ??= _operationCommandFactory.CreateRestartCommand(_operationStateMachine);

  public AddToScriptViewCommand AddToScriptViewCommand
    => _addToScriptViewCommand ??= _operationCommandFactory.CreateAddToScriptViewCommand(this);

  public ICommand RemoveOperationFromScriptCommand
    => _removeOperationFromScriptCommand ??= _operationCommandFactory.CreateRemoveOperationFromScriptCommand(this);

  public string Name { get; }

  [UsedImplicitly]
  public string ComponentInstanceName { get; }

  [UsedImplicitly]
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
    get => _stateString;
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
    set
    {
      StopOperationCommand.CanExecuteValue = value;
      RestartOperationCommand.CanExecuteValue = value;
    }
  }


  public void Start()
  {
    _operationStateMachine.Start();
  }

  public void Initial()
  {
    _operationStateMachine.Initial(this);
  }

  public void NotifyOnCurrentState(string stateName, Runnability runnability, ErrorInfo errorInfo)
  {
    CanRun = runnability.CanRun;
    CanStop = runnability.CanStop;
    State = stateName;
    LastErrorFullText = errorInfo.LastErrorFullText;
    LastError = errorInfo.LastError;
  }

  public void Ready()
  {
    _operationStateMachine.Ready(this);
  }

  public void Success()
  {
    _operationStateMachine.Success(this);
  }

  public void DependencyFulfilled()
  {
    _operationStateMachine.DependencyFulfilled(this);
  }

  public void Stopped()
  {
    _operationStateMachine.Stopped(this);
    RestartOperationCommand.ContinueIfNeeded();
  }

  public void Failure(Exception exception)
  {
    _operationStateMachine.Failure(exception, this);
  }

  public void InProgress(CancellationTokenSource cancellationTokenSource)
  {
    _operationStateMachine.InProgress(this, cancellationTokenSource);
  }

  public void SetPropertiesOn(OperationPropertiesViewModel operationPropertiesViewModel)
  {
    operationPropertiesViewModel.Properties = _propertyListBuilder.RetrieveList();
  }

  #region Boilerplate
  public event PropertyChangedEventHandler PropertyChanged;

  [NotifyPropertyChangedInvocator]
  protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
  #endregion

  public void ObserveDependencies(IEnumerable<OperationViewModel> operationViewModel)
  {
    if (_maybeDependencyName.HasValue)
    {
      var dependencyName = _maybeDependencyName.Value();
      var observedOperation = FindOperationWith(dependencyName, operationViewModel);
      observedOperation._operationStateMachine.FromNowOnReportSuccessfulExecutionTo(this);
    }
  }

  private static OperationViewModel FindOperationWith(string dependencyName, IEnumerable<OperationViewModel> operationViewModel)
  {
    return operationViewModel.First(o => o.IsKnownAs(dependencyName));
  }

  private bool IsKnownAs(string name) => Name == name;

  public void ObserveOperationState()
  {
    _operationStateMachine.RegisterContext(this);
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
//TODO component settings

