using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
      CanRun = false;
      CanStop = true;
      State = "Success";
      LastErrorFullText = LastError = string.Empty;
      _operationState = new ExecutableOperationState();
    }

    public void Failure(Exception exception)
    {
      CanRun = false;
      CanStop = true;
      State = "Failure";
      LastErrorFullText = exception.ToString();
      LastError = exception.ToString().Split(
        new[] { Environment.NewLine },
        StringSplitOptions.RemoveEmptyEntries).First();

      _operationState = new ExecutableOperationState();
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

  public class BadOperation : Operation
  {
    public Task RunAsync() => Task.Run(() => { throw new Exception("Lolokimono"); });
  }
}

//TODO dependent operations