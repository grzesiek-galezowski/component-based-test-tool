using System.Threading;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
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
      OperationSignals operation)
    {
      return new StopOperationCommand(_applicationContext, operation);
    }

    public RestartOperationCommand CreateRestartCommand(OperationSignals operation, OperationViewModel operationViewModel)
    {
      return new RestartOperationCommand(_applicationContext, operation, operationViewModel);
    }
  }

  public class RestartOperationCommand : OperationCommand
  {
    private readonly OperationSignals _operation;
    private readonly OperationViewModel _operationViewModel;
    private bool _waitingForStart;

    public RestartOperationCommand(
      ApplicationContext applicationContext, 
      OperationSignals operation, 
      OperationViewModel operationViewModel) 
      : base(false, applicationContext)
    {
      _operation = operation;
      _operationViewModel = operationViewModel;
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
        _operation.Start(_operationViewModel);
      }
    }
  }
}