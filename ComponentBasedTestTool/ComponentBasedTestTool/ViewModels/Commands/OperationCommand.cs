using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using ComponentBasedTestTool.Annotations;

namespace ComponentBasedTestTool.ViewModels.Commands
{
  public abstract class OperationCommand : ICommand
  {
    protected bool _canExecute;

    protected OperationCommand(bool defaultCanExecute)
    {
      _canExecute = defaultCanExecute;
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
      Application.Current.Dispatcher.Invoke(() => CanExecuteChanged?.Invoke(this, EventArgs.Empty));
    }

    public event EventHandler CanExecuteChanged;
  }
}