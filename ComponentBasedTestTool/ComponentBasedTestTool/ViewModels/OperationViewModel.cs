using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.GlueCode;
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
    private OperationParametersViewModelListBuilder _parametersListBuilder;

    public OperationViewModel(string name, Operation operation)
    {
      Name = name;
      this.Ready();
      _operation = operation;
      _parametersListBuilder = new OperationParametersViewModelListBuilder();
      _operation.FillParameters(_parametersListBuilder);

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
      operationParametersViewModel.OperationParameters = _parametersListBuilder.BuildList();
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

  public class OperationParametersViewModelListBuilder : OperationParametersListBuilder
  {
    private readonly List<OperationParameterViewModel> _operationParameterViewModels;

    public OperationParametersViewModelListBuilder()
    {
      _operationParameterViewModels = new List<OperationParameterViewModel>();
    }

    public IList<OperationParameterViewModel> BuildList()
    {
      return _operationParameterViewModels;
    }

    public OperationParameter<string> Path(string name, string defaultValue)
    {
      var operationParameterViewModel = new OperationParameterViewModel { Option = name, Value = defaultValue };
      _operationParameterViewModels.Add(operationParameterViewModel);
      return ViewModelBasedPathParameter.Containing(operationParameterViewModel);
    }

    public OperationParameter<bool> Flag(string name, bool defaultValue)
    {
      var operationParameterViewModel = new OperationParameterViewModel { Option = name, Value = defaultValue.ToString() };
      _operationParameterViewModels.Add(operationParameterViewModel);
      return ViewModelBasedFlagParameter.Containing(operationParameterViewModel);
    }

    public OperationParameter<TimeSpan> Seconds(string name, int amount)
    {
      var operationParameterViewModel = new OperationParameterViewModel { Option = name, Value = amount.ToString() };
      _operationParameterViewModels.Add(operationParameterViewModel);
      return new ViewModelBasedParameter<TimeSpan>(operationParameterViewModel, s => TimeSpan.FromSeconds(int.Parse(s)));
    }
  }

  //TODO consider making this and above a generic class
}

//TODO dependent operations