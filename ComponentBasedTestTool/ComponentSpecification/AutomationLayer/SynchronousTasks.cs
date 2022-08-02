using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentSpecification.AutomationLayer;

public class SynchronousTasks : IBackgroundTasks
{
  private readonly Action<IOperationContext> _setRunningOperationContext;

  public SynchronousTasks(Action<IOperationContext> setRunningOperationContext)
  {
    _setRunningOperationContext = setRunningOperationContext;
  }

  public void Run(IRunnable operation, IOperationContext context)
  {
    _setRunningOperationContext(context);
    using var token = new CancellationTokenSource();
    context.InProgress(token);
    operation.RunAsync(token.Token);
  }
}