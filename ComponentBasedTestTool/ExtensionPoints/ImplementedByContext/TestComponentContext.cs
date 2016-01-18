using ExtensionPoints.ImplementedByComponents;

namespace ExtensionPoints.ImplementedByContext
{
  public interface TestComponentContext
  {
    void AddOperation(string name, Operation operation, string dependencyName);
    void AddOperation(string name, Operation operation);
    OperationsOutput CreateOutFor(string operationName);
  }
}