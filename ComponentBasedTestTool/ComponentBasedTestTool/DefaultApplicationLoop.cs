using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using System.Xml.Linq;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views;
using ComponentBasedTestTool.Views.Ports;
using ComponentLoading.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ViewModels.ViewModels;

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
      var componentInstancesViewModel = new ComponentInstancesViewModel(operationsViewModel);
      var testComponentViewModelFactory =
        new TestComponentViewModelFactory(
          componentInstancesViewModel,
          outputFactory,
          new WpfOperationViewModelFactory(applicationContext, backgroundTasks));
      var componentsViewModel = new ComponentsViewModel(testComponentViewModelFactory);

      var topMenuBarViewModel = new TopMenuBarViewModel(
        componentInstancesViewModel,
        operationsOutputViewModel,
        componentsViewModel);

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

  public class TopMenuBarViewModel
  {
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;
    private readonly OperationsOutputViewModel _operationsOutputViewModel;
    private readonly ComponentsViewModel _componentsViewModel;

    public TopMenuBarViewModel(
      ComponentInstancesViewModel componentInstancesViewModel, 
      OperationsOutputViewModel operationsOutputViewModel, 
      ComponentsViewModel componentsViewModel)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _operationsOutputViewModel = operationsOutputViewModel;
      _componentsViewModel = componentsViewModel;
    }

    public ICommand SaveWorkspaceCommand => new SaveWorkspaceCommand(
      _componentInstancesViewModel, 
      _operationsOutputViewModel);
    public ICommand RestoreWorkspaceCommand => new RestoreWorkspaceCommand(
      _componentInstancesViewModel, 
      _operationsOutputViewModel,
      new RestoringOfSavedComponentsObserver(_operationsOutputViewModel));
  }

  public class RestoringOfSavedComponentsObserver
  {
    private readonly OperationsOutputViewModel _operationsOutputViewModel;

    public RestoringOfSavedComponentsObserver(OperationsOutputViewModel operationsOutputViewModel)
    {
      _operationsOutputViewModel = operationsOutputViewModel;
    }

    public void NotifyOnNewComponentWith(string name, string type)
    {
      _operationsOutputViewModel.WriteLine(name + ", " + type);
    }

    public void NotifyOnNextOperationWith(string name, string type)
    {
      _operationsOutputViewModel.WriteLine(" " + name + ", " + type);
    }

    public void NotifyOnPropertyWith(string name, string value)
    {
      _operationsOutputViewModel.WriteLine("  " + name + " --> " + value);
    }
  }

  public class RestoreWorkspaceCommand : ICommand
  {
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;
    private readonly OperationsOutputViewModel _operationsOutputViewModel;
    private readonly RestoringOfSavedComponentsObserver _restoringOfSavedComponentsObserver;

    public RestoreWorkspaceCommand(
      ComponentInstancesViewModel componentInstancesViewModel, 
      OperationsOutputViewModel operationsOutputViewModel, 
      RestoringOfSavedComponentsObserver restoringOfSavedComponentsObserver)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _operationsOutputViewModel = operationsOutputViewModel;
      _restoringOfSavedComponentsObserver = restoringOfSavedComponentsObserver;
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      var xDoc = XDocument.Load("Save.xml"); //bug redundancy
      foreach (var componentInstance in ComponentInstancesIn(xDoc))
      {
        _restoringOfSavedComponentsObserver.NotifyOnNewComponentWith(
          NameOf(componentInstance), TypeOf(componentInstance));
        ParseOperationsOf(componentInstance);
      }
    }

    private void ParseOperationsOf(XElement componentInstance)
    {
      foreach (var operation in OperationsOf(componentInstance))
      {
        _restoringOfSavedComponentsObserver.NotifyOnNextOperationWith(
          NameOf(operation), TypeOf(componentInstance));
        ParsePropertiesOf(operation);
      }
    }

    private void ParsePropertiesOf(XElement operation)
    {
      foreach (var property in ParametersOf(operation))
      {
        _restoringOfSavedComponentsObserver
          .NotifyOnPropertyWith(NameOf(property), ValueOf(property));
      }
    }

    private static IEnumerable<XElement> ParametersOf(XElement operation)
    {
      return operation.Elements("Parameter");
    }

    private static IEnumerable<XElement> ComponentInstancesIn(XDocument xDoc)
    {
      return xDoc.Root.Elements("Component");
    }

    private static IEnumerable<XElement> OperationsOf(XElement componentInstance)
    {
      return componentInstance.Elements("Operation");
    }

    private static string ValueOf(XElement property)
    {
      return property.Attribute("value").Value;
    }

    private static string NameOf(XElement property)
    {
      return property.Attribute("name").Value;
    }

    private string TypeOf(XElement componentInstance)
    {
      return componentInstance.Attribute("type").Value;
    }

    public event EventHandler CanExecuteChanged;
  }

  public class SaveWorkspaceCommand : ICommand
  {
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;
    private readonly OperationsOutputViewModel _operationsOutputViewModel;

    public SaveWorkspaceCommand(
      ComponentInstancesViewModel componentInstancesViewModel, 
      OperationsOutputViewModel operationsOutputViewModel)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _operationsOutputViewModel = operationsOutputViewModel;
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      _componentInstancesViewModel.SaveTo(new FileBasedPersistentStorage(_operationsOutputViewModel));
    }

    public event EventHandler CanExecuteChanged;

    protected virtual void OnCanExecuteChanged()
    {
      CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}