using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using System.Xml.Linq;
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
      var testComponentViewModelFactory =
        new TestComponentViewModelFactory(
          componentInstancesViewModel,
          outputFactory,
          new WpfOperationViewModelFactory(applicationContext));
      var componentsViewModel = new ComponentsViewModel(testComponentViewModelFactory);

      var topMenuBarViewModel = new TopMenuBarViewModel(
        componentInstancesViewModel,
        operationsOutputViewModel);

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
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;
    private readonly OperationsOutputViewModel _operationsOutputViewModel;

    public TopMenuBarViewModel(ComponentInstancesViewModel componentInstancesViewModel, OperationsOutputViewModel operationsOutputViewModel)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _operationsOutputViewModel = operationsOutputViewModel;
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

    public void NotifyOnNewComponent(string name)
    {
      _operationsOutputViewModel.WriteLine(name);
    }

    public void NotifyOnNextOperation(string name)
    {
      _operationsOutputViewModel.WriteLine(name);
    }

    public void NotifyOnProperty(string name, string value)
    {
      _operationsOutputViewModel.WriteLine(name);
      _operationsOutputViewModel.WriteLine(value);
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
      foreach (var componentInstance in xDoc.Root.Elements("Component"))
      {
        _restoringOfSavedComponentsObserver.NotifyOnNewComponent(componentInstance.Attribute("name").ToString());
        foreach (var operation in componentInstance.Elements("Operation"))
        {
          _restoringOfSavedComponentsObserver.NotifyOnNextOperation(operation.Attribute("name").ToString());
          foreach (var property in operation.Elements("Parameter"))
          {
            _restoringOfSavedComponentsObserver.NotifyOnProperty(
              property.Attribute("name").ToString(), 
              property.Attribute("value").ToString());
          }
        }
      }
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