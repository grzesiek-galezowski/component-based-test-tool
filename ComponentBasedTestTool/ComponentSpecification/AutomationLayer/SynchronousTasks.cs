using System;
using System.Threading;
using System.Threading.Tasks;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentSpecification.AutomationLayer;

public class SynchronousTasks : BackgroundTasks
{
  private readonly Action<OperationContext> _setRunningOperationContext;

  public SynchronousTasks(Action<OperationContext> setRunningOperationContext)
  {
    _setRunningOperationContext = setRunningOperationContext;
  }

  public void Run(Runnable operation, OperationContext context)
  {
    _setRunningOperationContext(context);
    using (var token = new CancellationTokenSource())
    {
      context.InProgress(token);
      operation.RunAsync(token.Token);
    }
  }
}