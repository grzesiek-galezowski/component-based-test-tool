using ExtensionPoints;

namespace Components
{
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