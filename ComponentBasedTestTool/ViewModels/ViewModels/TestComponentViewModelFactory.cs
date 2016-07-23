using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels
{
  public class TestComponentViewModelFactory
  {
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;
    private readonly OutputFactory _outputFactory;
    private readonly OperationViewModelFactory _operationViewModelFactory;
    private readonly BackgroundTasks _backgroundTasks;

    public TestComponentViewModelFactory(ComponentInstancesViewModel componentInstancesViewModel, 
      OutputFactory outputFactory, OperationViewModelFactory operationViewModelFactory, BackgroundTasks backgroundTasks)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _outputFactory = outputFactory;
      _operationViewModelFactory = operationViewModelFactory;
      _backgroundTasks = backgroundTasks;
    }

    public TestComponentViewModel Create(string name, string description, TestComponentInstanceFactory instanceFactory)
    {
      return new TestComponentViewModel(
        name, 
        description,
        _componentInstancesViewModel, 
        new ComponentInstanceViewModelFactory(
          instanceFactory, 
          _outputFactory, _operationViewModelFactory, _backgroundTasks));
    }
  }
}