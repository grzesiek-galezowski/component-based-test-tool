using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ViewModels.ViewModels
{
  public class AddAllSelectedCommand : ICommand
  {
    public bool CanExecute(object parameter) => true;

    public void Execute(object parameter)
    {
      var selectedComponents = (IEnumerable<TestComponentViewModel>) parameter;
      foreach (var testComponentViewModel in selectedComponents)
      {
        testComponentViewModel.AddComponentCommand.Execute(new object());
      }
    }

    public event EventHandler CanExecuteChanged;

    protected virtual void OnCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}