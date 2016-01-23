using System.Composition;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components
{
  [Export(typeof(TestComponentSourceRoot))]
  public class FileSystemComponentInstanceFactory : TestComponentInstanceFactory, TestComponentSourceRoot
  {
    public TestComponent Create()
    {
      return new FileSystemComponent();
    }

    public void AddTo(ComponentsList components)
    {
      components.Add("Filesystem", this);
    }
  }

}