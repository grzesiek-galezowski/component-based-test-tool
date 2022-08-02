using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ExtensionPoints.ImplementedByContext;

public interface ITestComponentContext
{
  IOperationsOutput CreateOutFor(string operationName);
  IOperationControl CreateOperation(IComponentOperation operation);
}