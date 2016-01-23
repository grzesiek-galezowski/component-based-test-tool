using System.Composition;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components
{
  [Export(typeof(TestComponentInstanceFactory))]
  public class FileSystemComponentInstanceFactory : TestComponentInstanceFactory
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