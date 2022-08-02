using ComponentBasedTestTool.Views.Ports;

namespace ComponentSpecification.AutomationLayer;

public class FakeApplicationContext : IApplicationContext
{
  public void Invoke(EventHandler eventHandler, object sender)
  {
    //for now left blank
  }
}