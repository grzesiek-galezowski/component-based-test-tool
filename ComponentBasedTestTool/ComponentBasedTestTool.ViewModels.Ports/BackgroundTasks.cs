using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.ViewModels.Ports;

public interface BackgroundTasks
{
  void Run(Runnable operation, OperationContext context);
}