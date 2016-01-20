using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Windows;
using ComponentBasedTestTool.Views;
using TriAxis.RunSharp;
using ViewModels.ViewModels;

namespace ComponentBasedTestTool
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow(
      OperationsViewModel operationsViewModel, 
      OperationsOutputViewModel operationsOutputViewModel, 
      OperationPropertiesViewModel operationPropertiesViewModel, 
      ComponentsViewModel componentsViewModel, 
      ComponentInstancesViewModel componentInstancesViewModel)
    {
      InitializeComponent();

      OperationPropertiesView.DataContext = operationPropertiesViewModel;
      OperationsView.DataContext = operationsViewModel;
      OperationsOutputView.DataContext = operationsOutputViewModel;
      ComponentsView.DataContext = componentsViewModel;
      ComponentsView.SetComponentInstancesViewDataContext(componentInstancesViewModel);
    }
  }
}
