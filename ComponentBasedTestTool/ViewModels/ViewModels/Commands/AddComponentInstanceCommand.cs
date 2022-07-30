using System;
using System.Windows.Input;
using ComponentBasedTestTool.Annotations;

namespace ViewModels.ViewModels.Commands;

public sealed class AddComponentInstanceCommand : ICommand
{
  private readonly ComponentInstancesViewModel _componentInstancesViewModel;
  private readonly ComponentInstanceViewModelFactory _componentInstanceViewModelFactory;
  private readonly TestComponentViewModel _testComponentViewModel;

  public AddComponentInstanceCommand(
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

  [UsedImplicitly]
  public void Execute(object parameter)
  {
    var componentInstanceViewModel = _componentInstanceViewModelFactory
      .CreateComponentInstanceViewModel(_testComponentViewModel);
    _componentInstancesViewModel.Add(componentInstanceViewModel);
  }

  private void OnCanExecuteChanged()
  {
    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
  }
}