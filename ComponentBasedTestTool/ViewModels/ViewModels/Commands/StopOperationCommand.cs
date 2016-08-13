using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Views.Ports;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

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