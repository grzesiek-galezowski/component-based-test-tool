using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain;

public interface IOperationState
{
  void Start(IOperationContext context, IRunnable operation);
  void DependencyFulfilled(IOperationContext operationViewModel);
  void Stop();
  void Notify(IOperationContext context);
}