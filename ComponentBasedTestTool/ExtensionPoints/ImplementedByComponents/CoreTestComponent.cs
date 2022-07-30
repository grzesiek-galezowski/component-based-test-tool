using ExtensionPoints.ImplementedByContext;

namespace ExtensionPoints.ImplementedByComponents;

public interface CoreTestComponent
{
  void PopulateOperations(TestComponentOperationDestination ctx);
  void CreateOperations(TestComponentContext ctx);
}