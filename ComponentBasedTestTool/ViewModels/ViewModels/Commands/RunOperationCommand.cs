using ComponentBasedTestTool.Views.Ports;

namespace ViewModels.ViewModels.Commands
{
  public class RunOperationCommand : OperationCommand
  {
    private readonly OperationViewModel _selectedOperation;

    public RunOperationCommand(OperationViewModel selectedOperation, 
      ApplicationContext applicationContext) : base(true, applicationContext)
    {
      _selectedOperation = selectedOperation;
    }

    public override void Execute(object parameter)
    {
      _selectedOperation.Start();
    }
  }
}