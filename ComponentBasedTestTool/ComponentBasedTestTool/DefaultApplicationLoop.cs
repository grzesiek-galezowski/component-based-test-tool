using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views;
using ComponentBasedTestTool.Views.Ports;
using ComponentLoading.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
using ViewModels.ViewModels;
using ViewModels.ViewModels.Commands;

namespace ComponentBasedTestTool
{
  public static class DefaultApplicationLoop
  {
    public static void Start(
      ApplicationBootstrap bootstrap, 
      ComponentLocation componentLocation, 
      ApplicationContext applicationContext, 
      BackgroundTasks backgroundTasks)
    {
      Configure(componentLocation, bootstrap, applicationContext, backgroundTasks);
      bootstrap.Start();
    }

    private static void Configure(
      ComponentLocation componentLocation, 
      ApplicationBootstrap bootstrap, 
      ApplicationContext applicationContext, 
      BackgroundTasks backgroundTasks)
    {
      var operationsOutputViewModel = new OperationsOutputViewModel();
      var operationPropertiesViewModel = new OperationPropertiesViewModel();
      var outputFactory = new OutputFactory(operationsOutputViewModel);
      var operationsViewModel = new OperationsViewModel(operationPropertiesViewModel);
      var componentInstancesViewModel = new ComponentInstancesViewModel(applicationContext, operationsViewModel);
      var operationMachinesByControlObject = new OperationMachinesByControlObject();
      var testComponentViewModelFactory =
        new TestComponentViewModelFactory(
          componentInstancesViewModel,
          outputFactory,
          new WpfOperationViewModelFactory(applicationContext, backgroundTasks), backgroundTasks, operationMachinesByControlObject);
      var componentsViewModel = new ComponentsViewModel(testComponentViewModelFactory);

      var topMenuBarViewModel = new TopMenuBarViewModel(
        componentInstancesViewModel,
        operationsOutputViewModel, new PersistentModelContentBuilderFactory(operationsOutputViewModel, operationMachinesByControlObject));

      var factoryRepositories = componentLocation.LoadComponentRoots();
      
      AddAllInstanceFactories(factoryRepositories, componentsViewModel);

      bootstrap.SetOperationPropertiesViewDataContext(operationPropertiesViewModel);
      bootstrap.SetTopMenuBarContext(topMenuBarViewModel);
      bootstrap.SetOperationsViewDataContext(operationsViewModel);
      bootstrap.SetOperationsOutputViewDataContext(operationsOutputViewModel);
      bootstrap.SetComponentsViewDataContext(componentsViewModel);
      bootstrap.SetComponentInstancesViewDataContext(componentInstancesViewModel);
      return;
    }

    private static void AddAllInstanceFactories(
      IEnumerable<TestComponentSourceRoot> factoryRepositories, 
      ComponentsList componentsViewModel)
    {
      foreach (var testComponentInstanceFactoryRepository in factoryRepositories)
      {
        testComponentInstanceFactoryRepository.AddTo(componentsViewModel);
      }
    }
  }
}