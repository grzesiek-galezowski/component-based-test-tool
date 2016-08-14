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
    private readonly OperationMachinesByControlObject _operationMachinesByControlObject;

    public TestComponentViewModelFactory(
      ComponentInstancesViewModel componentInstancesViewModel, 
      OutputFactory outputFactory, 
      OperationViewModelFactory operationViewModelFactory, 
      BackgroundTasks backgroundTasks, 
      OperationMachinesByControlObject operationMachinesByControlObject)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _outputFactory = outputFactory;
      _operationViewModelFactory = operationViewModelFactory;
      _backgroundTasks = backgroundTasks;
      _operationMachinesByControlObject = operationMachinesByControlObject;
    }

    public TestComponentViewModel Create(string name, string description, TestComponentInstanceFactory instanceFactory)
    {
      return new TestComponentViewModel(
        name, 
        description,
        _componentInstancesViewModel, 
        new ComponentInstanceViewModelFactory(
          instanceFactory, 
          _outputFactory, _operationViewModelFactory, _backgroundTasks, _operationMachinesByControlObject));
    }
  }
}