using System.Collections.Generic;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;

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
      var testComponentInstance = _instanceFactory.Create();
      var componentInstanceViewModel 
        = new ComponentInstanceViewModel(
          testComponentViewModel.Name, 
          _outputFactory, 
          new OperationEntries());

      testComponentInstance.PopulateOperations(componentInstanceViewModel);
      componentInstanceViewModel.Initialize(_operationViewModelFactory);

      return componentInstanceViewModel;
    }
  }
}