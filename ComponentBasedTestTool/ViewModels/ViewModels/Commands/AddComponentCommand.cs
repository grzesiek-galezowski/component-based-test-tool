using System;
using System.Windows.Input;

namespace ViewModels.ViewModels.Commands
{
  public sealed class AddComponentCommand : ICommand
  {
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;
    private readonly ComponentInstanceViewModelFactory _componentInstanceViewModelFactory;
    private readonly TestComponentViewModel _testComponentViewModel;

    public AddComponentCommand(
      ComponentInstancesViewModel componentInstancesViewModel, 
      ComponentInstanceViewModelFactory componentInstanceViewModelFactory, 
      TestComponentViewModel testComponentViewModel)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _componentInstanceViewModelFactory = componentInstanceViewModelFactory;
      _testComponentViewModel = testComponentViewModel;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      var componentInstanceViewModel = _componentInstanceViewModelFactory
        .CreateComponentInstanceViewModel(_testComponentViewModel);
      _componentInstancesViewModel.ComponentInstances.Add(componentInstanceViewModel);
    }

    private void OnCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}