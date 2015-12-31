using System;
using System.Windows.Input;

namespace ComponentBasedTestTool.ViewModels.Commands
{
  public class RunOperationCommand : OperationCommand
  {
    private readonly OperationViewModel _selectedOperation;

    public RunOperationCommand(OperationViewModel selectedOperation) : base(true)
    {
      _selectedOperation = selectedOperation;
    }

    public override void Execute(object parameter)
    {
      _selectedOperation.Run();
    }
  }
}