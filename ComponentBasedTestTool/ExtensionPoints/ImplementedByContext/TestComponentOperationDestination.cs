using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ExtensionPoints.ImplementedByContext
{
  public interface TestComponentOperationDestination
  {
    void AddOperation(string name, OperationStateMachine operation, string dependencyName);
    void AddOperation(string name, OperationStateMachine operation);
  }
}