using System.Composition;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components;

[Export(typeof(TestComponentSourceRoot))]
[UsedImplicitly]
public class FileSystemComponentInstanceFactory : TestComponentInstanceFactory, TestComponentSourceRoot
{
  public CoreTestComponent Create()
  {
    return new FileSystemComponent();
  }

  public void AddTo(ComponentsList components)
  {
    components.Add("Filesystem", "Performs various filesystem operations", this);
  }
}