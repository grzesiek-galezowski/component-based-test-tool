using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Views.Ports;
using ViewModels.ViewModels;
using ViewModels.ViewModels.Commands;

namespace ViewModels.Composition
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
      OperationSignals operation)
    {
      return new StopOperationCommand(_applicationContext, operation);
    }

    public RestartOperationCommand CreateRestartCommand(OperationSignals operation)
    {
      return new RestartOperationCommand(_applicationContext, operation);
    }
  }
}