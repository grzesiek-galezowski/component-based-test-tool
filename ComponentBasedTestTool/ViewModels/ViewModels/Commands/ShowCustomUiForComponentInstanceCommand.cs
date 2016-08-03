using System;
using System.Windows.Input;
using ExtensionPoints.ImplementedByComponents;

namespace ViewModels.ViewModels.Commands
{
  public sealed class ShowCustomUiForComponentInstanceCommand : ICommand
  {
    private readonly TestComponent _testComponentInstance;

    public ShowCustomUiForComponentInstanceCommand(TestComponent testComponentInstance)
    {
      _testComponentInstance = testComponentInstance;
    }

    public bool CanExecute(object parameter)
    {
      return true; //for now
    }

    public void Execute(object parameter)
    {
      _testComponentInstance.ShowCustomUi();
    }

    public event EventHandler CanExecuteChanged;

    private void OnCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}