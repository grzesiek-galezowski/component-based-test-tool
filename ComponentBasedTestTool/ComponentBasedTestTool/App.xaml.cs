using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ComponentBasedTestTool.Views;
using Components;
using ViewModels.ViewModels;
using ViewModels.ViewModels.Commands;

namespace ComponentBasedTestTool
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      var operationsOutputViewModel = new OperationsOutputViewModel();
      var operationPropertiesViewModel = new OperationPropertiesViewModel();
      var outputFactory = new OutputFactory(operationsOutputViewModel);
      var operationsViewModel = new OperationsViewModel(operationPropertiesViewModel);
      var componentInstancesViewModel = new ComponentInstancesViewModel(operationsViewModel);
      var testComponentViewModelFactory = 
        new TestComponentViewModelFactory(
          componentInstancesViewModel, 
          outputFactory, 
          new WpfOperationViewModelFactory(new WpfApplicationContext()));
      var componentsViewModel = new ComponentsViewModel(testComponentViewModelFactory);

      var fileSystemComponentFactory = new FileSystemComponentInstanceFactory();
      fileSystemComponentFactory.AddTo(componentsViewModel);

      var mainWindow = new MainWindow();
      mainWindow.SetOperationPropertiesViewDataContext(operationPropertiesViewModel);
      mainWindow.SetOperationsViewDataContext(operationsViewModel);
      mainWindow.SetOperationsOutputViewDataContext(operationsOutputViewModel);
      mainWindow.SetComponentsViewDataContext(componentsViewModel);
      mainWindow.SetComponentInstancesViewDataContext(componentInstancesViewModel);

      mainWindow.Show();
    }
  }
}
