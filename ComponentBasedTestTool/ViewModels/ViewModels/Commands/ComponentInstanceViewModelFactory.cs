using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
using ExtensionPoints.ImplementedByComponents;
using ViewModels.Composition;

namespace ViewModels.ViewModels.Commands;

public class ComponentInstanceViewModelFactory
{
  private readonly ITestComponentInstanceFactory _componentInstanceFactory;
  private readonly OutputFactory _outputFactory;
  private readonly IOperationViewModelFactory _operationViewModelFactory;
  private readonly IBackgroundTasks _backgroundTasks;
  private readonly OperationMachinesByControlObject _operationMachinesByControlObject;
  private readonly IApplicationEvents _applicationEvents;

  public ComponentInstanceViewModelFactory(
    ITestComponentInstanceFactory componentInstanceFactory, OutputFactory outputFactory, 
    IOperationViewModelFactory operationViewModelFactory, IBackgroundTasks backgroundTasks, 
    OperationMachinesByControlObject operationMachinesByControlObject, 
    IApplicationEvents applicationEvents)
  {
    _componentInstanceFactory = componentInstanceFactory;
    _outputFactory = outputFactory;
    _operationViewModelFactory = operationViewModelFactory;
    _backgroundTasks = backgroundTasks;
    _operationMachinesByControlObject = operationMachinesByControlObject;
    _applicationEvents = applicationEvents;
  }

  private int _id = 1;

  public ComponentInstanceViewModel CreateComponentInstanceViewModel(TestComponentViewModel testComponentViewModel)
  {
    var testComponentInstance = _componentInstanceFactory.Create();
    var nullCapabilities = new NullCapabilities();
    var interfaceCasts = new InterfaceCasts(testComponentInstance);
    var customGuiCapability = interfaceCasts.To<Capabilities.ICustomGui>(nullCapabilities);
    var customClosingCapability = interfaceCasts.To<Capabilities.ICleanupOnEnvironmentClosing>(nullCapabilities);

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
    return testComponentViewModel.Name + _id++;
  }
}