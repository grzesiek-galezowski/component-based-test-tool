using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.ViewModels.Ports;

public interface IBackgroundTasks
{
  void Run(IRunnable operation, IOperationContext context);
}