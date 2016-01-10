using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.ViewModels.Commands;
using ComponentBasedTestTool.ViewModels.OperationStates;
using ExtensionPoints;

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
    private readonly OperationPropertiesViewModelBuilder _propertyListBuilder;
    private object _cachedObject = null;

    public OperationViewModel(string name, Operation operation)
    {
      Name = name;
      this.Ready();
      _operation = operation;
      _propertyListBuilder = new OperationPropertiesViewModelBuilder(name);
      _operation.FillParameters(_propertyListBuilder);

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
  }
}

//TODO dependent operations
//TODO what happens when we remove operation while it is in progress?
//TODO component view and script view
//TODO communication view like wireshark
//TODO in component addition, create button called "Add Selected"