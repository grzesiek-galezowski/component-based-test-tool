using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using ComponentLoading.Ports;
using ExtensionPoints.ImplementedByComponents;
using Jal.AssemblyFinder.Impl;

namespace ComponentLoading;

public class ExecutingAssemblyFolder : ComponentLocation
{
  public IEnumerable<TestComponentSourceRoot> LoadComponentRoots()
  {
    var configuration = LoadPluginAssemblies();

    using (var container = configuration.CreateContainer())
    {
      return container.GetExports<TestComponentSourceRoot>();
    }
  }

  private static ContainerConfiguration LoadPluginAssemblies()
  {
    var directory = AppDomain.CurrentDomain.BaseDirectory;
    AssemblyFinder.Current = new AssemblyFinder(directory);

    var assemblies = AssemblyFinder.Current.GetAssemblies("CBTS-PLUGIN");

    if (!assemblies.Any())
    {
      throw new Exception($"No plugins found in {directory}, exiting");
    }

    var configuration =
      new ContainerConfiguration()
        .WithAssemblies(assemblies);
    return configuration;
  }
}