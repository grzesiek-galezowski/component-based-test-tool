using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Views.Ports;

namespace ViewModels.ViewModels.Commands;

public class StopOperationCommand : OperationCommand
{
  private readonly IOperationSignals _operation;

  public StopOperationCommand(
    IApplicationContext applicationContext, 
    IOperationSignals operation) 
    : base(false, applicationContext)
  {
    _operation = operation;
  }

  public override void Execute(object parameter)
  {
    _operation.Stop();
  }

}