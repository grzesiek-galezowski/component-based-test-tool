using ExtensionPoints;

namespace ViewModels.ViewModels.Commands
{
  public class ComponentInstanceViewModelFactory
  {
    private readonly TestComponentInstanceFactory _instanceFactory;
    private readonly OutputFactory _outputFactory;
    private readonly OperationViewModelFactory _operationViewModelFactory;

    public ComponentInstanceViewModelFactory(
      TestComponentInstanceFactory instanceFactory, 
      OutputFactory outputFactory, 
      OperationViewModelFactory operationViewModelFactory)
    {
      _instanceFactory = instanceFactory;
      _outputFactory = outputFactory;
      _operationViewModelFactory = operationViewModelFactory;
    }

    public ComponentInstanceViewModel CreateComponentInstanceViewModel(TestComponentViewModel testComponentViewModel)
    {
      return new ComponentInstanceViewModel(
        testComponentViewModel.Name, 
        _instanceFactory.Create(), _outputFactory, _operationViewModelFactory);
    }
  }
}