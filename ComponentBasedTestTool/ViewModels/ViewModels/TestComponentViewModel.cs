using System.Windows.Input;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels
{
  public class TestComponentViewModel
  {
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;
    private readonly ComponentInstanceViewModelFactory _componentInstanceViewModelFactory;

    public TestComponentViewModel(
      string name, 
      ComponentInstancesViewModel componentInstancesViewModel, 
      ComponentInstanceViewModelFactory componentInstanceViewModelFactory)
    {
      Name = name;
      _componentInstancesViewModel = componentInstancesViewModel;
      _componentInstanceViewModelFactory = componentInstanceViewModelFactory;
    }

    public ICommand AddComponentInstanceCommand => 
      new AddComponentInstanceCommand(
        _componentInstancesViewModel, 
        _componentInstanceViewModelFactory, this);

    public string Name { get; set; }
  }
}