using System.ComponentModel;
using System.Windows;
using ComponentBasedTestTool.Views.Ports;
using ComponentBasedTestTool.Views.Views;

namespace ComponentBasedTestTool.Views
{


  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window, ApplicationBootstrap
  {
    public MainWindow()
    {
      InitializeComponent();
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

  }
}
