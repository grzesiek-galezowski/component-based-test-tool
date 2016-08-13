using ExtensionPoints.ImplementedByContext;

namespace ExtensionPoints.ImplementedByComponents
{
  public interface TestComponent : TestComponentAdditionalCapabilities.CustomGui
  {
    void PopulateOperations(TestComponentOperationDestination ctx);
    void CreateOperations(TestComponentContext testComponentContext);
    void ShowCustomUi();
  }
}