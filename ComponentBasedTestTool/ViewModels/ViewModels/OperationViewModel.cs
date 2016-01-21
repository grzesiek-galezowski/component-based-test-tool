﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using CallMeMaybe;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints;
using ExtensionPoints.ImplementedByContext;
using ViewModels.ViewModels.Commands;
using ViewModels.ViewModels.OperationStates;

namespace ViewModels.ViewModels
{
  public class OperationViewModel  : INotifyPropertyChanged, OperationContext, OperationDependencyObserver
  {
    private string _stateString;
    private OperationCommand _runCommand;
    private OperationCommand _stopCommand;
    private string _lastError = string.Empty;
    private string _lastErrorFullText = "lolokimono";
    private readonly Maybe<string> _maybeDependencyName;
    private readonly OperationPropertiesViewModelBuilder _propertyListBuilder;
    private object _cachedObject;
    private readonly OperationCommandFactory _operationCommandFactory;
    private readonly OperationStateMachine _operationStateMachine;

    public OperationViewModel(
      string name, 
      Maybe<string> maybeDependencyName, 
      OperationCommandFactory operationCommandFactory, 
      OperationPropertiesViewModelBuilder operationPropertiesViewModelBuilder, 
      OperationStateMachine operationStateMachine)
    {
      Name = name;
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


    public OperationCommand RunOperationCommand 
      => _runCommand ?? (_runCommand = _operationCommandFactory.CreateRunCommand(this));

    public OperationCommand StopOperationCommand
      => _stopCommand ?? (_stopCommand = 
      _operationCommandFactory.CreateStopCommand(_operationStateMachine));


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

    public void Start()
    {
      _operationStateMachine.Start(this);
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
    }

    public void Failure(Exception exception)
    {
      _operationStateMachine.Failure(exception, this);
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

