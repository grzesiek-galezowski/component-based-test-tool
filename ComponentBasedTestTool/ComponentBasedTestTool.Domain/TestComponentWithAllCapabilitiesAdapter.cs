using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ComponentBasedTestTool.Domain
{
  public class TestComponentWithAllCapabilitiesAdapter : TestComponent
  {
    private readonly CoreTestComponent _testComponentInstance;
    private readonly Capabilities.CustomGui _customGuiCapability;
    private readonly Capabilities.CleanupOnEnvironmentClosing _customClosingCapability;

    public TestComponentWithAllCapabilitiesAdapter(
      CoreTestComponent testComponentInstance, 
      Capabilities.CustomGui customGuiCapability, 
      Capabilities.CleanupOnEnvironmentClosing customClosingCapability)
    {
      _testComponentInstance = testComponentInstance;
      _customGuiCapability = customGuiCapability;
      _customClosingCapability = customClosingCapability;
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

    public void CleanupOnClosing()
    {
      _customClosingCapability.CleanupOnClosing();
    }
  }
}