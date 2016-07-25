using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ExtensionPoints.ImplementedByContext
{
  public interface TestComponentContext
  {
    OperationsOutput CreateOutFor(string operationName);
    OperationControl CreateOperation(ComponentOperation operation);
  }
}