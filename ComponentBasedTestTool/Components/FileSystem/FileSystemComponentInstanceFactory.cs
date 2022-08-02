using System.Composition;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components.FileSystem;

[Export(typeof(ITestComponentSourceRoot))]
[UsedImplicitly]
public class FileSystemComponentInstanceFactory : ITestComponentInstanceFactory, ITestComponentSourceRoot
{
  public ICoreTestComponent Create()
  {
    return new FileSystemComponent();
  }

  public void AddTo(IComponentsList components)
  {
    components.Add("Filesystem", "Performs various filesystem operations", this);
  }
}