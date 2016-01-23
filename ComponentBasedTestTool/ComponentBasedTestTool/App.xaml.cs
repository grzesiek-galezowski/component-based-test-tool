using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using System.Windows;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.Views;
using ComponentBasedTestTool.Views.Ports;
using ExtensionPoints.ImplementedByComponents;
using Jal.AssemblyFinder.Impl;
using ViewModels.ViewModels;

namespace ComponentBasedTestTool
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup([CanBeNull] StartupEventArgs e)
    {
      base.OnStartup(e);

      DefaultApplicationLoop.Start(new MainWindow(), new ExecutingAssemblyFolder());
    }
  }
}
