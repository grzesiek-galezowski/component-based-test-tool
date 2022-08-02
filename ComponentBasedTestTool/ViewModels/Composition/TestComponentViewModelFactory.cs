using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
using ExtensionPoints.ImplementedByComponents;
using ViewModels.ViewModels;
using ViewModels.ViewModels.Commands;

namespace ViewModels.Composition;

public class TestComponentViewModelFactory
{
  private readonly ComponentInstancesViewModel _componentInstancesViewModel;
  private readonly OutputFactory _outputFactory;
  private readonly IOperationViewModelFactory _operationViewModelFactory;
  private readonly IBackgroundTasks _backgroundTasks;
  private readonly OperationMachinesByControlObject _operationMachinesByControlObject;
  private readonly IApplicationEvents _applicationEvents;

  public TestComponentViewModelFactory(
    ComponentInstancesViewModel componentInstancesViewModel, 
    OutputFactory outputFactory, 
    IOperationViewModelFactory operationViewModelFactory, 
    IBackgroundTasks backgroundTasks, 
    OperationMachinesByControlObject operationMachinesByControlObject, 
    IApplicationEvents applicationEvents)
  {
    _componentInstancesViewModel = componentInstancesViewModel;
    _outputFactory = outputFactory;
    _operationViewModelFactory = operationViewModelFactory;
    _backgroundTasks = backgroundTasks;
    _operationMachinesByControlObject = operationMachinesByControlObject;
    _applicationEvents = applicationEvents;
  }


  public TestComponentViewModel Create(string name, string description, ITestComponentInstanceFactory instanceFactory)
  {
    return new TestComponentViewModel(
      name, 
      description,
      _componentInstancesViewModel, 
      new ComponentInstanceViewModelFactory(
        instanceFactory, 
        _outputFactory, 
        _operationViewModelFactory, 
        _backgroundTasks, 
        _operationMachinesByControlObject,
        _applicationEvents));
  }
}