using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ComponentBasedTestTool.ViewModels;
using Components;

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
      var operationsViewModel = new OperationsViewModel(operationPropertiesViewModel, outputFactory);

      new FileSystemComponent().PopulateOperations(operationsViewModel);

      new MainWindow(
        operationsViewModel,
        operationsOutputViewModel, 
        operationPropertiesViewModel).Show();
    }
  }
}
