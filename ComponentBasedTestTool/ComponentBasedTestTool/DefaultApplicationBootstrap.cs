using ComponentBasedTestTool.Views;
using ComponentBasedTestTool.Views.Ports;
using ViewModels.ViewModels;

namespace ComponentBasedTestTool
{
  public static class DefaultApplicationBootstrap
  {
    public static void Start(ApplicationBootstrap bootstrap, PluginLocation pluginLocation)
    {
      Configure(pluginLocation, bootstrap);
      bootstrap.Start();
    }

    private static void Configure(PluginLocation pluginLocation, ApplicationBootstrap mainWindow)
    {
      var operationsOutputViewModel = new OperationsOutputViewModel();
      var operationPropertiesViewModel = new OperationPropertiesViewModel();
      var outputFactory = new OutputFactory(operationsOutputViewModel);
      var operationsViewModel = new OperationsViewModel(operationPropertiesViewModel);
      var componentInstancesViewModel = new ComponentInstancesViewModel(operationsViewModel);
      var testComponentViewModelFactory =
        new TestComponentViewModelFactory(
          componentInstancesViewModel,
          outputFactory,
          new WpfOperationViewModelFactory(new WpfApplicationContext()));
      var componentsViewModel = new ComponentsViewModel(testComponentViewModelFactory);

      var factoryRepositories = pluginLocation.LoadComponentRoots();
      
      foreach (var testComponentInstanceFactoryRepository in factoryRepositories)
      {
        testComponentInstanceFactoryRepository.AddTo(componentsViewModel);
      }

      ApplicationBootstrap bootstrap = mainWindow;
      bootstrap.SetOperationPropertiesViewDataContext(operationPropertiesViewModel);
      bootstrap.SetOperationsViewDataContext(operationsViewModel);
      bootstrap.SetOperationsOutputViewDataContext(operationsOutputViewModel);
      bootstrap.SetComponentsViewDataContext(componentsViewModel);
      bootstrap.SetComponentInstancesViewDataContext(componentInstancesViewModel);
      return;
    }
  }
}