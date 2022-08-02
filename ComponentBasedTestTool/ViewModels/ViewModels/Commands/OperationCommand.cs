using System;
using System.Windows.Input;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.Views.Ports;

namespace ViewModels.ViewModels.Commands;

public abstract class OperationCommand : ICommand
{
  private bool _canExecute;
  private readonly IApplicationContext _applicationContext;

  protected OperationCommand(bool defaultCanExecute, IApplicationContext applicationContext)
  {
    _canExecute = defaultCanExecute;
    _applicationContext = applicationContext;
  }

  public bool CanExecuteValue
  {
    get { return _canExecute; }
    set
    {
      if (value != _canExecute)
      {
        _canExecute = value;
        OnCanExecuteChanged();
      }
    }
  }

  public bool CanExecute([NotNull] object parameter)
  {
    return CanExecuteValue;
  }

  public abstract void Execute(object parameter);

  protected void OnCanExecuteChanged()
  {
    _applicationContext.Invoke(CanExecuteChanged, this);
  }

  public event EventHandler CanExecuteChanged;
}