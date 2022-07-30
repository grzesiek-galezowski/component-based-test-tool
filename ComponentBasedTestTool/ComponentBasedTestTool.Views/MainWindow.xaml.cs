using System;
using System.Windows;
using ComponentBasedTestTool.Views.Ports;

namespace ComponentBasedTestTool.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, ApplicationBootstrap
{
  public MainWindow()
  {
    InitializeComponent();
    base.Closing += (_, _) => OnEnvironmentClosing();
  }

  public void SetComponentInstancesViewDataContext(object componentInstancesViewModel)
  {
    ComponentsView.SetComponentInstancesViewDataContext(componentInstancesViewModel);
  }

  public void SetComponentsViewDataContext(object componentsViewModel)
  {
    ComponentsView.DataContext = componentsViewModel;
  }

  public void SetOperationsOutputViewDataContext(object operationsOutputViewModel)
  {
    OperationsOutputView.DataContext = operationsOutputViewModel;
  }

  public void SetOperationsViewDataContext(object operationsViewModel)
  {
    OperationsView.DataContext = operationsViewModel;
  }

  public void SetScriptOperationsViewDataContext(object scriptOperationsViewModel)
  {
    ScriptOperationsView.DataContext = scriptOperationsViewModel;
  }

  public void SetOperationsViewsViewDataContext(object operationViewsViewModel)
  {
    OperationViewsView.DataContext = operationViewsViewModel;
  }

  public void SetOperationPropertiesViewDataContext(object operationPropertiesViewModel)
  {
    OperationPropertiesView.DataContext = operationPropertiesViewModel;
  }

  public void Start()
  {
    Show();
  }

  public void SetTopMenuBarContext(object topMenuBarContextViewModel)
  {
    TopMenuBar.DataContext = topMenuBarContextViewModel;
  }

  public event Action EnvironmentClosing;

  protected virtual void OnEnvironmentClosing()
  {
    EnvironmentClosing?.Invoke();
  }
}