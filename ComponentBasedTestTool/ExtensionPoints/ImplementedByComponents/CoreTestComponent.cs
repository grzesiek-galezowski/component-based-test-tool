using ExtensionPoints.ImplementedByContext;

namespace ExtensionPoints.ImplementedByComponents;

public interface ICoreTestComponent
{
  void PopulateOperations(ITestComponentOperationDestination ctx);
  void CreateOperations(ITestComponentContext ctx);
}