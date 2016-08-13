using System;

namespace ComponentBasedTestTool.Views.Ports
{
  public interface ApplicationContext
  {
    void Invoke(EventHandler eventHandler, object sender);
  }
}