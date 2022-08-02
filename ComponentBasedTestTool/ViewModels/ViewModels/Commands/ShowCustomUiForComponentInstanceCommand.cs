using System;
using System.Windows.Input;
using ExtensionPoints.ImplementedByComponents;

namespace ViewModels.ViewModels.Commands;

public sealed class ShowCustomUiForComponentInstanceCommand : ICommand
{
  private readonly Capabilities.ICustomGui _componentInstance;

  public ShowCustomUiForComponentInstanceCommand(Capabilities.ICustomGui componentInstance)
  {
    _componentInstance = componentInstance;
  }

  public bool CanExecute(object parameter)
  {
    return true; //for now
  }

  public void Execute(object parameter)
  {
    _componentInstance.ShowCustomUi();
  }

  public event EventHandler CanExecuteChanged;

  private void OnCanExecuteChanged()
  {
    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
  }
}