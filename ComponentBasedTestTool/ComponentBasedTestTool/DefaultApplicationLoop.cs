using System.Collections.Generic;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
using ComponentLoading.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ViewModels.Composition;
using ViewModels.ViewModels;
using ViewModels.ViewModels.Commands;
using ViewModelsGlueCode.Interfaces;

namespace ComponentBasedTestTool;

public class DefaultApplicationLoop
{
  public void Start(
    IApplicationBootstrap bootstrap, 
    IComponentLocation componentLocation, 
    IApplicationContext applicationContext, 
    IBackgroundTasks backgroundTasks)
  {
    Configure(componentLocation, bootstrap, applicationContext, backgroundTasks);
    bootstrap.Start();
  }

  private static void Configure(
    IComponentLocation componentLocation, 
    IApplicationBootstrap bootstrap, 
    IApplicationContext applicationContext, 
    IBackgroundTasks backgroundTasks)
  {
    var operationsOutputViewModel = new OperationsOutputViewModel();
    var operationPropertiesViewModel = new OperationPropertiesViewModel();
    var scriptOperationsViewModel = new ScriptOperationsViewModel(operationPropertiesViewModel);
    var operationsViewModel = new OperationsViewModel(operationPropertiesViewModel);
    var operationViewsViewModel = new OperationViewsViewModel(new IOperationsViewInitialization[] {operationsViewModel, scriptOperationsViewModel});
    var componentInstancesViewModel = new ComponentInstancesViewModel(operationsViewModel, operationViewsViewModel);
    var operationMachinesByControlObject = new OperationMachinesByControlObject();
    var outputFactory = new OutputFactory(operationsOutputViewModel);
    var testComponentViewModelFactory =
      new TestComponentViewModelFactory(
        componentInstancesViewModel,
        outputFactory,
        new WpfOperationViewModelFactory(applicationContext, scriptOperationsViewModel, new PropertySetBuilderFactory()), 
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
    bootstrap.SetOperationsViewsViewDataContext(operationViewsViewModel);
    return;
  }

  private static void AddAllInstanceFactories(
    IEnumerable<ITestComponentSourceRoot> factoryRepositories, 
    IComponentsList componentsViewModel)
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