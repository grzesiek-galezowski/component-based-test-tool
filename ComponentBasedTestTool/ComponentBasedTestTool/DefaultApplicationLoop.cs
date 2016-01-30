using System;
using System.Linq.Expressions;
using System.Windows.Input;
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
      _componentInstancesViewModel, _operationsOutputViewModel);
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