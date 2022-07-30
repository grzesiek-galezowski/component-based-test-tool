using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain;

public interface OperationState
{
  void Start(OperationContext context, Runnable operation);
  void DependencyFulfilled(OperationContext operationViewModel);
  void Stop();
  void Notify(OperationContext context);
}