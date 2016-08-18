using System;
using System.Collections.Generic;
using ComponentBasedTestTool;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
using ComponentLoading.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
using NSubstitute;
using NSubstitute.Core;
using TddEbook.TddToolkit;
using ViewModels.ViewModels;
using Xunit;

namespace ComponentSpecification.AutomationLayer
{
  public class ComponentBasedTestToolDriver : ApplicationBootstrap
  {
    private ComponentInstancesViewModel _componentInstancesViewModel;

    private readonly List<FakeComponentInstance> _fakeComponentInstances = new List<FakeComponentInstance>();
    private ComponentsViewModel _componentsViewModel;
    private readonly TestComponentInstanceFactory _instanceFactory;
    private OperationPropertiesViewModel _operationPropertiesViewModel;
    private OperationsOutputViewModel _operationsOutputViewModel;
    private OperationsViewModel _operationsViewModel;
    private Maybe<OperationContext> _runningOperationContext = Maybe.Nothing<OperationContext>();
    private OperationsViewModel _scriptOperationsViewModel;

    public ComponentBasedTestToolDriver()
    {
      _instanceFactory = Substitute.For<TestComponentInstanceFactory>();
      ComponentInstances = new FakeComponentInstances(_fakeComponentInstances);
      ComponentsSetup = new FakeTestComponents(_fakeComponentInstances);
    }

    public FakeTestComponents ComponentsSetup { get; }
    public FakeComponentsView ComponentsView => new FakeComponentsView(_componentsViewModel);
    public FakeInstancesView InstancesView => new FakeInstancesView(_componentInstancesViewModel);
    public FakeOperationsView OperationsView => new FakeOperationsView(_operationsViewModel);
    public FakePropertiesView PropertiesView => new FakePropertiesView(_operationPropertiesViewModel);
    public FakeOperationsState Operations => new FakeOperationsState(
      _componentInstancesViewModel.SelectedInstance.InstanceName,
      _operationsViewModel.SelectedOperation.Name,
      ComponentsSetup, _runningOperationContext);

    public FakeComponentInstances ComponentInstances { get; }
    public FakeScriptView ScriptView => new FakeScriptView(_scriptOperationsViewModel);

    public void AssertNoComponentsAreLoaded()
    {
      Assert.Equal(0, _componentsViewModel.TestComponents.Count);
    }

    public void StartApplication()
    {
      var bootstrap = this;
      var pluginLocation = Substitute.For<ComponentLocation>();
      var componentRoot = Substitute.For<TestComponentSourceRoot>();

      componentRoot.When(m => m.AddTo(Arg.Any<ComponentsList>())).Do(AddConfiguredComponents());

      pluginLocation.LoadComponentRoots().Returns(new[] {componentRoot});

      new DefaultApplicationLoop().Start(
        bootstrap, 
        pluginLocation, 
        new FakeApplicationContext(), 
        new SynchronousTasks(SetRunningOperationContext));
    }

    private void SetRunningOperationContext(OperationContext context)
    {
      _runningOperationContext = Maybe.Just(context);
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

    public void SetScriptOperationsViewDataContext(object scriptOperationsViewModel)
    {
      _scriptOperationsViewModel = (OperationsViewModel)scriptOperationsViewModel;
    }

    void ApplicationBootstrap.Start()
    {
      // Method intentionally left empty.
    }

    public void SetTopMenuBarContext(object topMenuBarContextViewModel)
    {
      
    }


    public void StartOperation(string componentName1, string operationName11)
    {
      InstancesView.Select(componentName1);
      OperationsView.Select(operationName11);
      OperationsView.StartSelectedOperation();
    }

    public event Action EnvironmentClosing;

    public void Close()
    {
      EnvironmentClosing.Invoke();
    }
  }

  public class FakeScriptView
  {
    private readonly OperationsViewModel _scriptOperationsViewModel;

    public FakeScriptView(OperationsViewModel scriptOperationsViewModel)
    {
      _scriptOperationsViewModel = scriptOperationsViewModel;
    }

    public void AssertContainsNoOperations()
    {
      Assert.Empty(_scriptOperationsViewModel.Operations);
    }
  }
}