using System.Collections.Generic;
using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool
{
  public interface ComponentLocation
  {
    IEnumerable<TestComponentSourceRoot> LoadComponentRoots();
  }
}