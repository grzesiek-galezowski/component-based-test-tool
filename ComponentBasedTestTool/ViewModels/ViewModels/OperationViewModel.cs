using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using CallMeMaybe;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints;
using ViewModels.ViewModels.Commands;
using ViewModels.ViewModels.OperationStates;

namespace ViewModels.ViewModels
{


  public class OperationViewModel  : INotifyPropertyChanged, OperationContext
  {
    private string _stateString;
    private OperationState _operationState;
    private OperationCommand _runCommand;
    private OperationCommand _stopCommand;
    private string _lastError = string.Empty;
    private string _lastErrorFullText = "lolokimono";
    private readonly Operation _operation;
    private readonly Maybe<string> _maybeDependencyName;
    private readonly OperationPropertiesViewModelBuilder _propertyListBuilder;
    private object _cachedObject;
    private readonly OperationCommandFactory _operationCommandFactory;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly List<OperationViewModel> _observers = new List<OperationViewModel>();

    public OperationViewModel(string name, Operation operation, Maybe<string> maybeDependencyName, OperationCommandFactory operationCommandFactory)
    {
      _cancellationTokenSource = new CancellationTokenSource();
      Name = name;
      _operation = operation;
      _maybeDependencyName = maybeDependencyName;
      _propertyListBuilder = new OperationPropertiesViewModelBuilder(name);
      _operationCommandFactory = operationCommandFactory;

      _operation.FillParameters(_propertyListBuilder);
      if (_maybeDependencyName.HasValue)
      {
        this.Initial();
      }
      else
      {
        this.Ready();
      }
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
      _operationState.Run(this, _operation);
    }

    public void Initial()
    {
      CanRun = false;
      CanStop = false;
      State = "Initial";
      LastErrorFullText = LastError = string.Empty;
      _operationState = new NotExecutableOperationState();
    }

    public void Ready() { NormalExecutable("Ready"); }

    public void Success()
    {
      NormalExecutable("Success");
      foreach (var observer in _observers)
      {
        observer.DependencyFulfilled();
      }
    }

    public void DependencyFulfilled()
    {
      _operationState.DependencyFulfilled(this);
    }

    public void Stopped() { NormalExecutable("Stopped"); }

    public void Failure(Exception exception)
    {
      CanRun = true;
      CanStop = false;
      State = "Failure";
      LastErrorFullText = exception.ToString();
      LastError = exception.ToString().Split(
        new[] { Environment.NewLine },
        StringSplitOptions.RemoveEmptyEntries).First();

      _operationState = ExecutableState();
    }


    public void InProgress()
    {
      CanRun = false;
      CanStop = true;
      State = "In Progress";
      LastErrorFullText = LastError = string.Empty;
      _operationState = new InProgressOperationState();
    }

    public void SetPropertiesOn(OperationPropertiesViewModel operationPropertiesViewModel)
    {
      operationPropertiesViewModel.Properties = 
        _cachedObject ?? (_cachedObject = _propertyListBuilder.Build());
    }

    private ExecutableOperationState ExecutableState()
    {
      return new ExecutableOperationState(_cancellationTokenSource);
    }

    private void NormalExecutable(string statusText)
    {
      CanRun = true;
      CanStop = false;
      State = statusText;
      LastErrorFullText = LastError = string.Empty;
      _operationState = ExecutableState();
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
        observedOperation.FromNowOnReportSuccessfulExecutionTo(this);
      }
    }

    private static OperationViewModel FindOperationWith(string dependencyName, IEnumerable<OperationViewModel> operationViewModel)
    {
      return operationViewModel.First(o => o.IsKnownAs(dependencyName));
    }

    private void FromNowOnReportSuccessfulExecutionTo(OperationViewModel observer)
    {
      _observers.Add(observer);
    }

    private bool IsKnownAs(string name) => Name == name;
  }
}

//TODO dependent operations
//TODO script view
//TODO removing components TODO what happens when we remove operation while it is in progress?
//TODO component view and script view
//TODO communication view like wireshark
//TODO in component addition, create button called "Add All Selected"
//TODO persistence