using System;
using System.Windows;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.Views.Ports;

namespace ComponentBasedTestTool.Views;

public class WpfApplicationContext : IApplicationContext
{
  public void Invoke([CanBeNull] EventHandler eventHandler, object sender)
  {
    Application.Current.Dispatcher.Invoke(() => eventHandler?.Invoke(sender, EventArgs.Empty));
  }
}