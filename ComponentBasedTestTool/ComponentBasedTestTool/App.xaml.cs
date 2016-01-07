using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ComponentBasedTestTool.ViewModels;
using Components;
using ExtensionPoints;

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
      var operationsViewModel = new OperationsViewModel(operationPropertiesViewModel);

      operationsViewModel.Operations.Add(new OperationViewModel("ls", new LsOperation(Out("ls", operationsOutputViewModel))));
      operationsViewModel.Operations.Add(new OperationViewModel("cd", new CdOperation     (Out("cd", operationsOutputViewModel))));
      operationsViewModel.Operations.Add(new OperationViewModel("cat", new CatOperation   (Out("cat", operationsOutputViewModel))));
      operationsViewModel.Operations.Add(new OperationViewModel("sleep", new WaitOperation(Out("sleep", operationsOutputViewModel))));

      new MainWindow(
        operationsViewModel,
        operationsOutputViewModel, 
        operationPropertiesViewModel).Show();
    }

    private OperationsOutput Out(string operationName, OperationsOutput output)
    {
      return new FormattingOperationOutput(operationName, output);
    }
  }

  class FormattingOperationOutput : OperationsOutput
  {
    private readonly string _operationName;
    private readonly OperationsOutput _output;

    public FormattingOperationOutput(string operationName, OperationsOutput output)
    {
      _operationName = operationName;
      _output = output;
    }

    public void WriteLine(string text)
    {
      _output.WriteLine($"[{_operationName}]" + ": " + text);
    }
  }
}
