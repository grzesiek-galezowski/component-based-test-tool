using System;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain.OperationStates
{
  public sealed class RunnableOperationState : OperationState
  {
    private readonly BackgroundTasks _inBackground;

    public RunnableOperationState(BackgroundTasks backgroundTasks)
    {
      _inBackground = backgroundTasks;
    }

    public void Start(OperationContext context, Runnable operation)
    {
      _inBackground.Run(operation, context);
    }

    public void DependencyFulfilled(OperationContext operationViewModel)
    {
      
    }

    public void Stop()
    {
      throw new NotImplementedException();
    }

    public void Notify(OperationContext context)
    {
      context.Ready();
    }
  }
}