using System.Windows;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.Views;
using ComponentLoading;

namespace ComponentBasedTestTool;

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
    applicationBootstrap.Closing += (_, _) => defaultApplicationLoop.Stop();
    defaultApplicationLoop.Start(
      applicationBootstrap, 
      new ExecutingAssemblyFolder(), 
      new WpfApplicationContext(), 
      new AsyncBasedBackgroundTasks());
  }
}