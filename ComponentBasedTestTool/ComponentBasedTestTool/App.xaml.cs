using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using System.Windows;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.Views;
using ComponentBasedTestTool.Views.Ports;
using ComponentLoading;
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

      var applicationBootstrap = new MainWindow();
      var defaultApplicationLoop = new DefaultApplicationLoop();
      applicationBootstrap.Closing += (sender, args) => defaultApplicationLoop.Stop();
      defaultApplicationLoop.Start(
        applicationBootstrap, 
        new ExecutingAssemblyFolder(), 
        new WpfApplicationContext(), 
        new AsyncBasedBackgroundTasks());
    }
  }
}
