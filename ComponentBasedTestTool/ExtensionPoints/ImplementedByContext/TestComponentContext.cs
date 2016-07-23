using ExtensionPoints.ImplementedByComponents;

namespace ExtensionPoints.ImplementedByContext
{
  public interface PersistentStorage
  {
    void Store(params Persistable[] persistables);
    void StoreValue<T>(string name, T value);
  }

  public interface TestComponentOperationDestination
  {
    void AddOperation(string name, OperationStateMachine operation, string dependencyName);
    void AddOperation(string name, OperationStateMachine operation);
  }

  public interface Persistable
  {
    void StoreIn(PersistentStorage persistentStorage);
  }

  public interface TestComponentContext
  {
    OperationsOutput CreateOutFor(string operationName);
    OperationStateMachine CreateOperation(ComponentOperation operation);
  }
}