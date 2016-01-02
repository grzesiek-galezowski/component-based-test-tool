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

      var operationsViewModel = new OperationsViewModel();

      operationsViewModel.Operations.Add(new OperationViewModel("ls", new LsOperation(operationsOutputViewModel)));
      operationsViewModel.Operations.Add(new OperationViewModel("cd", new CdOperation(operationsOutputViewModel)));
      operationsViewModel.Operations.Add(new OperationViewModel("cat", new CatOperation(operationsOutputViewModel)));

      var operationParametersViewModel = new OperationParametersViewModel();
      operationParametersViewModel.OperationParameters
        .Add(new OperationParameterViewModel() { Option="IP", Value = "127.0.0.1" });

      new MainWindow(
        operationsViewModel,
        operationsOutputViewModel,
        operationParametersViewModel).Show();
    }
  }
}
