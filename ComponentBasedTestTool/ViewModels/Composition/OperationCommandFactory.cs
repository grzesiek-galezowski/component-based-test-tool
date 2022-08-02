using System.Windows.Input;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Views.Ports;
using ViewModels.ViewModels;
using ViewModels.ViewModels.Commands;

namespace ViewModels.Composition;

public class OperationCommandFactory
{
  private readonly IApplicationContext _applicationContext;
  private readonly ScriptOperationsViewModel _scriptOperationsViewModel;

  public OperationCommandFactory(IApplicationContext applicationContext, 
    ScriptOperationsViewModel scriptOperationsViewModel)
  {
    _applicationContext = applicationContext;
    _scriptOperationsViewModel = scriptOperationsViewModel;
  }

  public RunOperationCommand CreateRunCommand(OperationViewModel operationViewModel)
  {
    return new RunOperationCommand(operationViewModel, _applicationContext);
  }

  public StopOperationCommand CreateStopCommand(
    IOperationSignals operation)
  {
    return new StopOperationCommand(_applicationContext, operation);
  }

  public RestartOperationCommand CreateRestartCommand(IOperationSignals operation)
  {
    return new RestartOperationCommand(_applicationContext, operation);
  }

  public AddToScriptViewCommand CreateAddToScriptViewCommand(OperationViewModel operationViewModel)
  {
    return new AddToScriptViewCommand(_scriptOperationsViewModel, operationViewModel);
  }

  public ICommand CreateRemoveOperationFromScriptCommand(OperationViewModel operationViewModel)
  {
    return new RemoveOperationFromScriptCommand(operationViewModel, _scriptOperationsViewModel);
  }
}