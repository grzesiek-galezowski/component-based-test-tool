using System.Windows.Input;
using ComponentBasedTestTool.ViewModels.Commands;

namespace ComponentBasedTestTool.ViewModels
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
      //bug bind it differently:
      _componentInstanceViewModelFactory = 
        componentInstanceViewModelFactory;
    }

    public ICommand AddComponentCommand => 
      new AddComponentCommand(
        _componentInstancesViewModel, 
        _componentInstanceViewModelFactory, this);

    public string Name { get; set; }
  }
}