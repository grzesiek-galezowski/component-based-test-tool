using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByComponents;

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
  }
}