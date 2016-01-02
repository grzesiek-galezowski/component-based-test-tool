using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.ViewModels.Commands;
using ComponentBasedTestTool.ViewModels.OperationStates;

namespace ComponentBasedTestTool.ViewModels
{
  public class OperationViewModel  : INotifyPropertyChanged
  {
    private string _stateString;
    private OperationState _operationState;
    private OperationCommand _runCommand;
    private OperationCommand _cancelCommand;
    private string _lastError = string.Empty;
    private string _lastErrorFullText = "lolokimono";
    private readonly Operation _operation;

    public OperationViewModel(string name, Operation operation)
    {
      Name = name;
      this.Ready();
      _operation = operation;
    }

    public OperationCommand RunOperationCommand 
      => _runCommand ?? (_runCommand = new RunOperationCommand(this));
    public OperationCommand CancelOperationCommand
      => _cancelCommand ?? (_cancelCommand = new CancelOperationCommand());

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
      get { return CancelOperationCommand.CanExecuteValue; }
      set { CancelOperationCommand.CanExecuteValue = value; }
    }

    public void Run()
    {
      _operationState.Run(this, _operation);
    }

    public void Ready()
    {
      CanRun = true;
      CanStop = false;
      State = "Ready";
      LastErrorFullText = LastError = string.Empty;
      _operationState = new ExecutableOperationState();
    }

    public void InProgress()
    {
      CanRun = false;
      CanStop = true;
      State = "In Progress";
      LastErrorFullText = LastError = string.Empty;
      _operationState = new NotExecutableOperationState();
    }

    public void Success()
    {
      CanRun = true;
      CanStop = false;
      State = "Success";
      LastErrorFullText = LastError = string.Empty;
      _operationState = new ExecutableOperationState();
    }

    public void Failure(Exception exception)
    {
      CanRun = true;
      CanStop = false;
      State = "Failure";
      LastErrorFullText = exception.ToString();
      LastError = exception.ToString().Split(
        new[] { Environment.NewLine },
        StringSplitOptions.RemoveEmptyEntries).First();

      _operationState = new ExecutableOperationState();
    }
    public void SetOperationsOn(OperationParametersViewModel operationParametersViewModel)
    {
      operationParametersViewModel.OperationParameters = new List<OperationParameterViewModel>()
      {
        new OperationParameterViewModel {Option = "IP", Value = "127.0.0.1"},
        new OperationParameterViewModel {Option = "IP2", Value = "127.0.0.2"},
      };
    }

    #region Boilerplate
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion


  }
}

//TODO dependent operations