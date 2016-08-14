using System;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ViewModels.Composition;

namespace ViewModels.ViewModels.Commands
{
  public class ComponentInstanceViewModelFactory
  {
    private readonly TestComponentInstanceFactory _instanceFactory;
    private readonly OutputFactory _outputFactory;
    private readonly OperationViewModelFactory _operationViewModelFactory;
    private readonly BackgroundTasks _backgroundTasks;
    private readonly OperationMachinesByControlObject _operationMachinesByControlObject;
    private readonly ApplicationEvents _applicationEvents;

    public ComponentInstanceViewModelFactory(
      TestComponentInstanceFactory instanceFactory, OutputFactory outputFactory, 
      OperationViewModelFactory operationViewModelFactory, BackgroundTasks backgroundTasks, 
      OperationMachinesByControlObject operationMachinesByControlObject, 
      ApplicationEvents applicationEvents)
    {
      _instanceFactory = instanceFactory;
      _outputFactory = outputFactory;
      _operationViewModelFactory = operationViewModelFactory;
      _backgroundTasks = backgroundTasks;
      _operationMachinesByControlObject = operationMachinesByControlObject;
      _applicationEvents = applicationEvents;
    }

    public ComponentInstanceViewModel CreateComponentInstanceViewModel(TestComponentViewModel testComponentViewModel)
    {
      var testComponentInstance = Wrap();

      var componentInstanceViewModel 
        = new ComponentInstanceViewModel(
          testComponentViewModel.Name, 
          _outputFactory, 
          new OperationEntries(_backgroundTasks),
          testComponentInstance, _backgroundTasks, 
          _operationMachinesByControlObject);

      componentInstanceViewModel.Initialize(_operationViewModelFactory);

      return componentInstanceViewModel;
    }

    private TestComponent Wrap()
    {
      var testComponentInstance = _instanceFactory.Create();
      var nullCapabilities = new NullCapabilities();
      var interfaceCasts = new InterfaceCasts(testComponentInstance);
      var customGuiCapability = interfaceCasts.To<Capabilities.CustomGui>(nullCapabilities);
      var customClosingCapability = interfaceCasts.To<Capabilities.CleanupOnEnvironmentClosing>(nullCapabilities);

      _applicationEvents.EnvironmentClosing += customClosingCapability.CleanupOnClosing;

      return new TestComponentWithAllCapabilitiesAdapter(
        testComponentInstance, 
        customGuiCapability,
        customClosingCapability);
    }
  }
}