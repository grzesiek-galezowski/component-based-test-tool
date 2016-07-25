using System;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;

namespace ViewModels.ViewModels.Commands
{
  public class ComponentInstanceViewModelFactory
  {
    private readonly TestComponentInstanceFactory _instanceFactory;
    private readonly OutputFactory _outputFactory;
    private readonly OperationViewModelFactory _operationViewModelFactory;
    private readonly BackgroundTasks _backgroundTasks;

    public ComponentInstanceViewModelFactory(
      TestComponentInstanceFactory instanceFactory, 
      OutputFactory outputFactory, 
      OperationViewModelFactory operationViewModelFactory, 
      BackgroundTasks backgroundTasks)
    {
      _instanceFactory = instanceFactory;
      _outputFactory = outputFactory;
      _operationViewModelFactory = operationViewModelFactory;
      _backgroundTasks = backgroundTasks;
    }

    public ComponentInstanceViewModel CreateComponentInstanceViewModel(TestComponentViewModel testComponentViewModel)
    {
      var testComponentInstance = _instanceFactory.Create();

      var componentInstanceViewModel 
        = new ComponentInstanceViewModel(
          testComponentViewModel.Name, 
          _outputFactory, 
          new OperationEntries(_backgroundTasks),
          testComponentInstance, _backgroundTasks);

      componentInstanceViewModel.Initialize(_operationViewModelFactory);

      return componentInstanceViewModel;
    }
  }
}