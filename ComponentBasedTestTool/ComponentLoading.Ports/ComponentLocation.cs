using System.Collections.Generic;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentLoading.Ports;

public interface IComponentLocation
{
  IEnumerable<ITestComponentSourceRoot> LoadComponentRoots();
}