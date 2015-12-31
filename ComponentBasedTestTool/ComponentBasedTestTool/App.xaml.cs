using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ComponentBasedTestTool.ViewModels;

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

      var operationsViewModel = new OperationsViewModel();

      operationsViewModel.Operations.Add(new OperationViewModel("ls", new LsOperation()));
      operationsViewModel.Operations.Add(new OperationViewModel("cd", new CdOperation()));
      operationsViewModel.Operations.Add(new OperationViewModel("cat", new CatOperation()));

      new MainWindow(
        operationsViewModel,
        operationsOutputViewModel).Show();
    }
  }
}
