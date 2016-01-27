using ComponentBasedTestTool.Views;
using ComponentBasedTestTool.Views.Ports;
using ComponentLoading.Ports;
using ViewModels.ViewModels;

namespace ComponentBasedTestTool
{
  public static class DefaultApplicationLoop
  {
    public static void Start(
      ApplicationBootstrap bootstrap, 
      ComponentLocation componentLocation, ApplicationContext applicationContext)
    {
      Configure(componentLocation, bootstrap, applicationContext);
      bootstrap.Start();
    }

    private static void Configure(
      ComponentLocation componentLocation, 
      ApplicationBootstrap bootstrap, 
      ApplicationContext applicationContext)
    {
      var operationsOutputViewModel = new OperationsOutputViewModel();
      var operationPropertiesViewModel = new OperationPropertiesViewModel();
      var outputFactory = new OutputFactory(operationsOutputViewModel);
      var operationsViewModel = new OperationsViewModel(operationPropertiesViewModel);
      var componentInstancesViewModel = new ComponentInstancesViewModel(operationsViewModel);
      var topMenuBarViewModel = new TopMenuBarViewModel();
      var testComponentViewModelFactory =
        new TestComponentViewModelFactory(
          componentInstancesViewModel,
          outputFactory,
          new WpfOperationViewModelFactory(applicationContext));
      var componentsViewModel = new ComponentsViewModel(testComponentViewModelFactory);

      var factoryRepositories = componentLocation.LoadComponentRoots();
      
      foreach (var testComponentInstanceFactoryRepository in factoryRepositories)
      {
        testComponentInstanceFactoryRepository.AddTo(componentsViewModel);
      }

      bootstrap.SetOperationPropertiesViewDataContext(operationPropertiesViewModel);
      bootstrap.SetTopMenuBarContext(topMenuBarViewModel);
      bootstrap.SetOperationsViewDataContext(operationsViewModel);
      bootstrap.SetOperationsOutputViewDataContext(operationsOutputViewModel);
      bootstrap.SetComponentsViewDataContext(componentsViewModel);
      bootstrap.SetComponentInstancesViewDataContext(componentInstancesViewModel);
      return;
    }
  }

  public class TopMenuBarViewModel
  {
  }
}