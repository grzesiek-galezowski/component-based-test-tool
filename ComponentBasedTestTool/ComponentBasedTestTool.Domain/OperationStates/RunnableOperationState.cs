using System;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain.OperationStates;

public sealed class RunnableOperationState : IOperationState
{
  private readonly IBackgroundTasks _inBackground;

  public RunnableOperationState(IBackgroundTasks backgroundTasks)
  {
    _inBackground = backgroundTasks;
  }

  public void Start(IOperationContext context, IRunnable operation)
  {
    _inBackground.Run(operation, context);
  }

  public void DependencyFulfilled(IOperationContext operationViewModel)
  {
      
  }

  public void Stop()
  {
    throw new NotImplementedException();
  }

  public void Notify(IOperationContext context)
  {
    context.Ready();
  }
}