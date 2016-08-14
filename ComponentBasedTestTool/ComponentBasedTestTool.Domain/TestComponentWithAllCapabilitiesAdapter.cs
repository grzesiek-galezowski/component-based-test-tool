using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ComponentBasedTestTool.Domain
{
  public class TestComponentWithAllCapabilitiesAdapter : TestComponent
  {
    private readonly CoreTestComponent _testComponentInstance;
    private readonly Capabilities.CustomGui _customGuiCapability;

    public TestComponentWithAllCapabilitiesAdapter(
      CoreTestComponent testComponentInstance, 
      Capabilities.CustomGui customGuiCapability)
    {
      _testComponentInstance = testComponentInstance;
      _customGuiCapability = customGuiCapability;
    }

    public void ShowCustomUi()
    {
      _customGuiCapability.ShowCustomUi();
    }

    public void PopulateOperations(TestComponentOperationDestination ctx)
    {
      _testComponentInstance.PopulateOperations(ctx);
    }

    public void CreateOperations(TestComponentContext ctx)
    {
      _testComponentInstance.CreateOperations(ctx);
    }
  }
}