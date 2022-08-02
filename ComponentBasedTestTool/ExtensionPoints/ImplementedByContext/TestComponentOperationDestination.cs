using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ExtensionPoints.ImplementedByContext;

public interface ITestComponentOperationDestination
{
  void AddOperation(string name, IOperationControl operation, string dependencyName);
  void AddOperation(string name, IOperationControl operation);
}