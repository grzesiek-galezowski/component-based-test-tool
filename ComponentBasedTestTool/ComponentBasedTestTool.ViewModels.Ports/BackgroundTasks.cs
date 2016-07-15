using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.ViewModels.Ports
{
  public interface BackgroundTasks
  {
    void Run(Runnable operation, OperationContext context);
  }
}