using System;
using ComponentBasedTestTool.Views.Ports;

namespace ComponentSpecification.AutomationLayer
{
  public class FakeApplicationContext : ApplicationContext
  {
    public void Invoke(EventHandler eventHandler, object sender)
    {
      //for now left blank
    }
  }
}