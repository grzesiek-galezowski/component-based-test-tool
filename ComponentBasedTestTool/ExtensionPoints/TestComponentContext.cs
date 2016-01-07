namespace ExtensionPoints
{
  public interface TestComponentContext
  {
    void AddOperation(string name, Operation operation);
    OperationsOutput CreateOutFor(string operationName);
  }
}