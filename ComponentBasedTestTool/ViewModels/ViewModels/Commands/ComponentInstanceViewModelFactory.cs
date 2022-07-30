using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
using ExtensionPoints.ImplementedByComponents;
using ViewModels.Composition;

namespace ViewModels.ViewModels.Commands;

public class ComponentInstanceViewModelFactory
{
  private readonly TestComponentInstanceFactory _componentInstanceFactory;
  private readonly OutputFactory _outputFactory;
  private readonly OperationViewModelFactory _operationViewModelFactory;
  private readonly BackgroundTasks _backgroundTasks;
  private readonly OperationMachinesByControlObject _operationMachinesByControlObject;
  private readonly ApplicationEvents _applicationEvents;

  public ComponentInstanceViewModelFactory(
    TestComponentInstanceFactory componentInstanceFactory, OutputFactory outputFactory, 
    OperationViewModelFactory operationViewModelFactory, BackgroundTasks backgroundTasks, 
    OperationMachinesByControlObject operationMachinesByControlObject, 
    ApplicationEvents applicationEvents)
  {
    _componentInstanceFactory = componentInstanceFactory;
    _outputFactory = outputFactory;
    _operationViewModelFactory = operationViewModelFactory;
    _backgroundTasks = backgroundTasks;
    _operationMachinesByControlObject = operationMachinesByControlObject;
    _applicationEvents = applicationEvents;
  }

  private int id = 1;

  public ComponentInstanceViewModel CreateComponentInstanceViewModel(TestComponentViewModel testComponentViewModel)
  {
    var testComponentInstance = _componentInstanceFactory.Create();
    var nullCapabilities = new NullCapabilities();
    var interfaceCasts = new InterfaceCasts(testComponentInstance);
    var customGuiCapability = interfaceCasts.To<Capabilities.CustomGui>(nullCapabilities);
    var customClosingCapability = interfaceCasts.To<Capabilities.CleanupOnEnvironmentClosing>(nullCapabilities);

    _applicationEvents.EnvironmentClosing += customClosingCapability.CleanupOnClosing;

    var componentInstanceViewModel 
      = new ComponentInstanceViewModel(
        GenerateInstanceName(testComponentViewModel), 
        _outputFactory, 
        new OperationEntries(_backgroundTasks),
        testComponentInstance, 
        _backgroundTasks, 
        _operationMachinesByControlObject, customGuiCapability);

    componentInstanceViewModel.Initialize(_operationViewModelFactory);

    return componentInstanceViewModel;
  }

  private string GenerateInstanceName(TestComponentViewModel testComponentViewModel)
  {
    return testComponentViewModel.Name + id++;
  }
}