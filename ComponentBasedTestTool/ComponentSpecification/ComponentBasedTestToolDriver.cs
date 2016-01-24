using System;
using System.Linq;
using ComponentBasedTestTool;
using ComponentBasedTestTool.Views.Ports;
using ComponentLoading.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using ViewModels.ViewModels;

namespace ComponentSpecification
{
  public class ComponentBasedTestToolDriver : ApplicationBootstrap
  {
    private ComponentInstancesViewModel _componentInstancesViewModel;

    private readonly FakeTestComponents _components = new FakeTestComponents();
    private ComponentsViewModel _componentsViewModel;
    private readonly TestComponentInstanceFactory _instanceFactory;
    private OperationPropertiesViewModel _operationPropertiesViewModel;
    private OperationsOutputViewModel _operationsOutputViewModel;
    private OperationsViewModel _operationsViewModel;

    public ComponentBasedTestToolDriver()
    {
      _instanceFactory = Substitute.For<TestComponentInstanceFactory>();
    }

    public FakeComponentsView ComponentsView => new FakeComponentsView(_componentsViewModel);
    public FakeInstancesView InstancesView => new FakeInstancesView(_componentInstancesViewModel);

    public void AddComponentsWithNames(params string[] names)
    {
      foreach (var name in names)
      {
        AddPluginWithName(name);
      }
    }

    public void AddInstanceOf(string componentName)
    {
      _componentsViewModel
        .TestComponents
        .First(c => c.Name == componentName)
        .AddComponentCommand.Execute(null);
    }

    public void AddPluginWithName(string name)
    {
      _components.Add(name);
    }

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

      DefaultApplicationLoop.Start(bootstrap, pluginLocation);
    }

    private Action<CallInfo> AddConfiguredComponents()
    {
      return ci =>
      {
        var componentsList = ((ComponentsList) ci[0]);
        _components.AddTo(componentsList);

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
  }
}