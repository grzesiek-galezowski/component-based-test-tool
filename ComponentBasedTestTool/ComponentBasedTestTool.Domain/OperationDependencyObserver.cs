namespace ComponentBasedTestTool.Domain;

public interface IOperationDependencyObserver
{
  void DependencyFulfilled();
}