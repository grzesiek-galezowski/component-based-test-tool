using System;

namespace ComponentBasedTestTool.Views.Ports;

public interface IApplicationContext
{
  void Invoke(EventHandler eventHandler, object sender);
}