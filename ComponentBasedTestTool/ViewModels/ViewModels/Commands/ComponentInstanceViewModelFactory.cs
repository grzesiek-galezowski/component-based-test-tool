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
    private readonly OperationMachinesByControlObject _operationMachinesByControlObject;

    public ComponentInstanceViewModelFactory(
      TestComponentInstanceFactory instanceFactory, 
      OutputFactory outputFactory, 
      OperationViewModelFactory operationViewModelFactory, 
      BackgroundTasks backgroundTasks, 
      OperationMachinesByControlObject operationMachinesByControlObject)
    {
      _instanceFactory = instanceFactory;
      _outputFactory = outputFactory;
      _operationViewModelFactory = operationViewModelFactory;
      _backgroundTasks = backgroundTasks;
      _operationMachinesByControlObject = operationMachinesByControlObject;
    }

    public ComponentInstanceViewModel CreateComponentInstanceViewModel(TestComponentViewModel testComponentViewModel)
    {
      var testComponentInstance = _instanceFactory.Create();

      var componentInstanceViewModel 
        = new ComponentInstanceViewModel(
          testComponentViewModel.Name, 
          _outputFactory, 
          new OperationEntries(_backgroundTasks),
          testComponentInstance, _backgroundTasks, _operationMachinesByControlObject);

      componentInstanceViewModel.Initialize(_operationViewModelFactory);

      return componentInstanceViewModel;
    }
  }
}