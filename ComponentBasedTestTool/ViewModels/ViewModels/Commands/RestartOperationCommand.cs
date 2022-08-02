using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Views.Ports;

namespace ViewModels.ViewModels.Commands;

public class RestartOperationCommand : OperationCommand
{
  private readonly IOperationSignals _operation;
  private bool _waitingForStart;

  public RestartOperationCommand(
    IApplicationContext applicationContext, 
    IOperationSignals operation) 
    : base(false, applicationContext)
  {
    _operation = operation;
    _waitingForStart = false;
  }

  public override void Execute(object parameter)
  {
    _operation.Stop();
    _waitingForStart = true;
  }

  public void ContinueIfNeeded()
  {
    if (_waitingForStart)
    {
      _waitingForStart = false;
      _operation.Start();
    }
  }
}