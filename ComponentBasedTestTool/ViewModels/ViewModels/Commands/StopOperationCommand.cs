using System.Threading;
using System.Windows;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;

namespace ViewModels.ViewModels.Commands
{
  public class StopOperationCommand : OperationCommand
  {
    private readonly OperationSignals _operation;

    public StopOperationCommand(
      ApplicationContext applicationContext, 
      OperationSignals operation) 
      : base(false, applicationContext)
    {
      _operation = operation;
    }

    public override void Execute(object parameter)
    {
      _operation.Stop();
    }

  }
}