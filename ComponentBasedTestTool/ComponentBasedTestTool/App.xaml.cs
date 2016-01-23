using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Windows;
using ComponentBasedTestTool.Views;
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
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      LoadComponentFactories();

      var mainWindow = CreateMainWindow();
      mainWindow.Show();
    }

    private static IEnumerable<TestComponentInstanceFactory> LoadComponentFactories()
    {
      var configuration = LoadPluginAssemblies();

      using (var container = configuration.CreateContainer())
      {
        return container.GetExports<TestComponentInstanceFactory>();
      }
    }

    private static ContainerConfiguration LoadPluginAssemblies()
    {
      var directory = AppDomain.CurrentDomain.BaseDirectory;
      AssemblyFinder.Current = new AssemblyFinder(directory);

      var assemblies = AssemblyFinder.Current.GetAssemblies("CBTS-PLUGIN");

      var configuration =
        new ContainerConfiguration()
          .WithAssemblies(assemblies);
      return configuration;
    }

    private static MainWindow CreateMainWindow()
    {
      var operationsOutputViewModel = new OperationsOutputViewModel();
      var operationPropertiesViewModel = new OperationPropertiesViewModel();
      var outputFactory = new OutputFactory(operationsOutputViewModel);
      var operationsViewModel = new OperationsViewModel(operationPropertiesViewModel);
      var componentInstancesViewModel = new ComponentInstancesViewModel(operationsViewModel);
      var testComponentViewModelFactory =
        new TestComponentViewModelFactory(
          componentInstancesViewModel,
          outputFactory,
          new WpfOperationViewModelFactory(new WpfApplicationContext()));
      var componentsViewModel = new ComponentsViewModel(testComponentViewModelFactory);

      var factories = LoadComponentFactories();
      foreach (var testComponentInstanceFactory in factories)
      {
        testComponentInstanceFactory.AddTo(componentsViewModel);
      }

      var mainWindow = new MainWindow();
      mainWindow.SetOperationPropertiesViewDataContext(operationPropertiesViewModel);
      mainWindow.SetOperationsViewDataContext(operationsViewModel);
      mainWindow.SetOperationsOutputViewDataContext(operationsOutputViewModel);
      mainWindow.SetComponentsViewDataContext(componentsViewModel);
      mainWindow.SetComponentInstancesViewDataContext(componentInstancesViewModel);
      return mainWindow;
    }
  }
}
