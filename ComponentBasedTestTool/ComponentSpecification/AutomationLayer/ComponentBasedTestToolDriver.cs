using System;
using ComponentBasedTestTool;
using ComponentBasedTestTool.Views.Ports;
using ComponentLoading.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using NUnit.Framework.Api;
using ViewModels.ViewModels;

namespace ComponentSpecification.AutomationLayer
{
  public class ComponentBasedTestToolDriver : ApplicationBootstrap
  {
    private ComponentInstancesViewModel _componentInstancesViewModel;

    private ComponentsViewModel _componentsViewModel;
    private readonly TestComponentInstanceFactory _instanceFactory;
    private OperationPropertiesViewModel _operationPropertiesViewModel;
    private OperationsOutputViewModel _operationsOutputViewModel;
    private OperationsViewModel _operationsViewModel;

    public ComponentBasedTestToolDriver()
    {
      _instanceFactory = Substitute.For<TestComponentInstanceFactory>();
    }

    public FakeTestComponents ComponentsSetup { get; } = new FakeTestComponents();
    public FakeComponentsView ComponentsView => new FakeComponentsView(_componentsViewModel);
    public FakeInstancesView InstancesView => new FakeInstancesView(_componentInstancesViewModel);
    public FakeOperationsView OperationsView => new FakeOperationsView(_operationsViewModel);
    public FakePropertiesView PropertiesView => new FakePropertiesView(_operationPropertiesViewModel);
    public FakeOperationAssertions Operations => new FakeOperationAssertions(
      _componentInstancesViewModel.SelectedInstance.InstanceName,
      _operationsViewModel.SelectedOperation.Name,
      ComponentsSetup);

    public void AssertNoComponentsAreLoaded()
    {
      Assert.AreEqual(0, _componentsViewModel.TestComponents.Count);
    }

    public void StartApplication()
    {
      var bootstrap = this;
      var pluginLocation = Substitute.For<ComponentLocation>();
      var componentRoot = Substitute.For<TestComponentSourceRoot>();

      componentRoot.When(m => m.AddTo(Arg.Any<ComponentsList>())).Do(AddConfiguredComponents());

      pluginLocation.LoadComponentRoots().Returns(new[] {componentRoot});

      DefaultApplicationLoop.Start(
        bootstrap, 
        pluginLocation, 
        new FakeApplicationContext(), 
        new SynchronousTasks());
    }

    private Action<CallInfo> AddConfiguredComponents()
    {
      return ci =>
      {
        var componentsList = ((ComponentsList) ci[0]);
        ComponentsSetup.AddTo(componentsList);

      };
    }

    void ApplicationBootstrap.SetComponentInstancesViewDataContext(object componentInstancesViewModel)
    {
      _componentInstancesViewModel = (ComponentInstancesViewModel) componentInstancesViewModel;
    }

    void ApplicationBootstrap.SetComponentsViewDataContext(object componentsViewModel)
    {
      _componentsViewModel = (ComponentsViewModel) componentsViewModel;
    }

    void ApplicationBootstrap.SetOperationPropertiesViewDataContext(object operationPropertiesViewModel)
    {
      _operationPropertiesViewModel = (OperationPropertiesViewModel) operationPropertiesViewModel;
    }

    void ApplicationBootstrap.SetOperationsOutputViewDataContext(object operationsOutputViewModel)
    {
      _operationsOutputViewModel = (OperationsOutputViewModel) operationsOutputViewModel;
    }

    void ApplicationBootstrap.SetOperationsViewDataContext(object operationsViewModel)
    {
      _operationsViewModel = (OperationsViewModel) operationsViewModel;
    }

    void ApplicationBootstrap.Start()
    {
      // Method intentionally left empty.
    }

    public void SetTopMenuBarContext(object topMenuBarContextViewModel)
    {
      
    }
  }

  public class FakeApplicationContext : ApplicationContext
  {
    public void Invoke(EventHandler eventHandler, object sender)
    {
      //for now left blank
    }
  }

  public class FakeOperationAssertions
  {
    private readonly string _instanceName;
    private readonly string _operationName;
    private readonly FakeTestComponents _componentsSetup;

    public FakeOperationAssertions(
      string instanceName, 
      string operationName, 
      FakeTestComponents componentsSetup)
    {
      _instanceName = instanceName;
      _operationName = operationName;
      _componentsSetup = componentsSetup;
    }

    public void AssertWasRun(string operationName, int count = 1)
    {
      _componentsSetup
        .RetrieveOperation(_instanceName, operationName)
        .AssertWasRun(count);
    }

    public void BehaveAsStoppedOnce(string operationName)
    {
      _componentsSetup.RetrieveOperation(_instanceName, operationName)
        .BehaveAsStoppedOnce();
    }

    public void BehaveAsSuccessfulOnce(string operationName)
    {
      _componentsSetup.RetrieveOperation(_instanceName, operationName)
        .BehaveAsSuccessfulOnce();

    }
  }
}