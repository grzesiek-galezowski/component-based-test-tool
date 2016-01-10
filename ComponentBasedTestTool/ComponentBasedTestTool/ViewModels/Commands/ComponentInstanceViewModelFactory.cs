using ExtensionPoints;

namespace ComponentBasedTestTool.ViewModels.Commands
{
  public class ComponentInstanceViewModelFactory
  {
    private readonly TestComponentInstanceFactory _instanceFactory;
    private readonly OutputFactory _outputFactory;

    public ComponentInstanceViewModelFactory(TestComponentInstanceFactory instanceFactory, OutputFactory outputFactory)
    {
      _instanceFactory = instanceFactory;
      _outputFactory = outputFactory;
    }

    public ComponentInstanceViewModel CreateComponentInstanceViewModel(TestComponentViewModel testComponentViewModel)
    {
      return new ComponentInstanceViewModel(
        testComponentViewModel.Name, 
        _instanceFactory.Create(), _outputFactory);
    }
  }
}