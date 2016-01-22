using System.Threading;
using ComponentBasedTestTool.ViewModels.Ports;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels
{
  public class OperationCommandFactory
  {
    private readonly ApplicationContext _applicationContext;

    public OperationCommandFactory(ApplicationContext applicationContext)
    {
      _applicationContext = applicationContext;
    }

    public RunOperationCommand CreateRunCommand(OperationViewModel operationViewModel)
    {
      return new RunOperationCommand(operationViewModel, _applicationContext);
    }

    public StopOperationCommand CreateStopCommand(
      OperationStateMachine operation)
    {
      return new StopOperationCommand(_applicationContext, operation);
    }
  }
}