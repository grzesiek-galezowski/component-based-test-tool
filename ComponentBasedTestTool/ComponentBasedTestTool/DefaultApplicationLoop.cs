using System.Collections.Generic;
using System.Windows;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views;
using ComponentBasedTestTool.Views.Ports;
using ComponentLoading.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
using ViewModels.Composition;
using ViewModels.ViewModels;
using ViewModels.ViewModels.Commands;

namespace ComponentBasedTestTool
{
  public class DefaultApplicationLoop
  {
    public void Start(
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
      var scriptOperationsViewModel = new OperationsViewModel(operationPropertiesViewModel);
      var operationsViewModel = new OperationsViewModel(operationPropertiesViewModel);
      var componentInstancesViewModel = new ComponentInstancesViewModel(operationsViewModel);
      var operationMachinesByControlObject = new OperationMachinesByControlObject();
      var outputFactory = new OutputFactory(operationsOutputViewModel);
      var testComponentViewModelFactory =
        new TestComponentViewModelFactory(
          componentInstancesViewModel,
          outputFactory,
          new WpfOperationViewModelFactory(applicationContext, scriptOperationsViewModel), 
          backgroundTasks, 
          operationMachinesByControlObject,
          bootstrap);
      var componentsViewModel = new ComponentsViewModel(testComponentViewModelFactory);

      var topMenuBarViewModel = new TopMenuBarViewModel(
        componentInstancesViewModel,
        operationsOutputViewModel, new PersistentModelContentBuilderFactory(operationsOutputViewModel, operationMachinesByControlObject));

      var factoryRepositories = componentLocation.LoadComponentRoots();
      
      AddAllInstanceFactories(factoryRepositories, componentsViewModel);

      bootstrap.SetOperationPropertiesViewDataContext(operationPropertiesViewModel);
      bootstrap.SetTopMenuBarContext(topMenuBarViewModel);
      bootstrap.SetOperationsViewDataContext(operationsViewModel);
      bootstrap.SetScriptOperationsViewDataContext(scriptOperationsViewModel);
      bootstrap.SetOperationsOutputViewDataContext(operationsOutputViewModel);
      bootstrap.SetComponentsViewDataContext(componentsViewModel);
      bootstrap.SetComponentInstancesViewDataContext(componentInstancesViewModel);
      OperationViewsViewModel operationViewsViewModel = new OperationViewsViewModel();
      bootstrap.SetOperationsViewsViewDataContext(operationViewsViewModel);
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

    public void Stop()
    {
      //TODO log
    }
  }
}