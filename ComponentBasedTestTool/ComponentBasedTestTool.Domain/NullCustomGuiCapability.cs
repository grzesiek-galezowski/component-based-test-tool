using System.Windows;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain
{
  public class NullCustomGuiCapability : Capabilities.CustomGui
  {
    public void ShowCustomUi()
    {
      //TODO refactor this away. This should have no dependency on UI
      MessageBox.Show("This plugin does not support custom GUI");
    }
  }
}