using ComponentBasedTestTool;
using ComponentBasedTestTool.Views.Ports;
using ComponentLoading.Ports;
using Core.Maybe;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
using NSubstitute;
using ViewModels.ViewModels;
using Xunit;

namespace ComponentSpecification.AutomationLayer;

public class ComponentBasedTestToolDriver : ApplicationBootstrap
{
  private Maybe<ComponentInstancesViewModel> _componentInstancesViewModel;

  private readonly List<FakeComponentInstance> _fakeComponentInstances = new();
  private Maybe<ComponentsViewModel> _componentsViewModel;
  private Maybe<OperationPropertiesViewModel> _operationPropertiesViewModel;
  private Maybe<OperationsViewModel> _operationsViewModel;
  private Maybe<OperationContext> _runningOperationContext = Maybe<OperationContext>.Nothing;
  private Maybe<ScriptOperationsViewModel> _scriptOperationsViewModel;

  public ComponentBasedTestToolDriver()
  {
    ComponentInstances = new FakeComponentInstances(_fakeComponentInstances);
    ComponentsSetup = new FakeTestComponents(_fakeComponentInstances);
    EnvironmentClosing += () => { };
  }

  public FakeTestComponents ComponentsSetup { get; }
  public FakeComponentsView ComponentsView => new(_componentsViewModel.Value());
  public FakeInstancesView InstancesView => new(_componentInstancesViewModel.Value());
  public FakeOperationsView OperationsView => new(_operationsViewModel.Value());
  public FakePropertiesView PropertiesView => new(_operationPropertiesViewModel.Value());
  public FakeOperationsState Operations => new(
    _componentInstancesViewModel.Value().SelectedInstance.InstanceName,
    _operationsViewModel.Value().SelectedOperation.Name,
    ComponentsSetup, _runningOperationContext);

  public FakeComponentInstances ComponentInstances { get; }
  public FakeScriptView ScriptView => new(_scriptOperationsViewModel.Value());

  public void AssertNoComponentsAreLoaded()
  {
    Assert.Empty(_componentsViewModel.Value().TestComponents);
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
    _runningOperationContext = context.Just();
  }

  private Action<NSubstitute.Core.CallInfo> AddConfiguredComponents()
  {
    return ci =>
    {
      var componentsList = ((ComponentsList) ci[0]);
      ComponentsSetup.AddTo(componentsList);
    };
  }

  void ApplicationBootstrap.SetComponentInstancesViewDataContext(object componentInstancesViewModel)
  {
    _componentInstancesViewModel = ((ComponentInstancesViewModel) componentInstancesViewModel).Just();
  }

  void ApplicationBootstrap.SetComponentsViewDataContext(object componentsViewModel)
  {
    _componentsViewModel = ((ComponentsViewModel) componentsViewModel).Just();
  }

  void ApplicationBootstrap.SetOperationPropertiesViewDataContext(object operationPropertiesViewModel)
  {
    _operationPropertiesViewModel = ((OperationPropertiesViewModel) operationPropertiesViewModel).Just();
  }

  void ApplicationBootstrap.SetOperationsOutputViewDataContext(object operationsOutputViewModel)
  {
  }

  void ApplicationBootstrap.SetOperationsViewDataContext(object operationsViewModel)
  {
    _operationsViewModel = ((OperationsViewModel) operationsViewModel).Just();
  }

  public void SetScriptOperationsViewDataContext(object scriptOperationsViewModel)
  {
    _scriptOperationsViewModel = ((ScriptOperationsViewModel)scriptOperationsViewModel).Just();
  }

  public void SetOperationsViewsViewDataContext(object operationViewsViewModel)
  {
      
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

  public void AddToScriptView(string componentName, string operationName)
  {
    InstancesView.Select(componentName);
    OperationsView.Select(operationName);
    OperationsView.AddSelectedOperationToScriptView();
  }
}

public class FakeScriptView
{
  private readonly ScriptOperationsViewModel _scriptOperationsViewModel;

  public FakeScriptView(ScriptOperationsViewModel scriptOperationsViewModel)
  {
    _scriptOperationsViewModel = scriptOperationsViewModel;
  }

  public void AssertContainsNoOperations()
  {
    Assert.Empty(_scriptOperationsViewModel.Operations);
  }

  public void AssertShowsExactly(params string[] operationNames)
  {
    Assert.Equal(
      operationNames,
      _scriptOperationsViewModel.Operations.Select(o => o.Name).ToArray());
  }

  public void Select(string componentInstanceName, string operationName)
  {
    _scriptOperationsViewModel.SelectedOperation = OperationViewByName(componentInstanceName, operationName);
  }

  private OperationViewModel OperationViewByName(string componentInstanceName, string operationName)
  {
    return _scriptOperationsViewModel.Operations.First(o => o.Name == operationName && o.ComponentInstanceName.StartsWith(componentInstanceName));
  }
}