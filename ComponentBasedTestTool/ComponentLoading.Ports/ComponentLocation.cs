using System.Collections.Generic;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentLoading.Ports;

public interface ComponentLocation
{
  IEnumerable<TestComponentSourceRoot> LoadComponentRoots();
}