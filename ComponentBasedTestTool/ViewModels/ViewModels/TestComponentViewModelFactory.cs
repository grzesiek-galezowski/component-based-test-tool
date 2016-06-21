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

    public TestComponentViewModelFactory(ComponentInstancesViewModel componentInstancesViewModel, OutputFactory outputFactory, OperationViewModelFactory operationViewModelFactory)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _outputFactory = outputFactory;
      _operationViewModelFactory = operationViewModelFactory;
    }

    public TestComponentViewModel Create(string name, string description, TestComponentInstanceFactory instanceFactory)
    {
      return new TestComponentViewModel(
        name, 
        description,
        _componentInstancesViewModel, 
        new ComponentInstanceViewModelFactory(
          instanceFactory, 
          _outputFactory, _operationViewModelFactory));
    }
  }
}