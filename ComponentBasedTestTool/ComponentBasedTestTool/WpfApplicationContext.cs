using System;
using System.Windows;
using ComponentBasedTestTool.Annotations;
using ViewModels.ViewModels.Commands;

namespace ComponentBasedTestTool
{
  public class WpfApplicationContext : ApplicationContext
  {
    public void Invoke([CanBeNull] EventHandler eventHandler, object sender)
    {
      Application.Current.Dispatcher.Invoke(() => eventHandler?.Invoke(sender, EventArgs.Empty));
    }
  }
}