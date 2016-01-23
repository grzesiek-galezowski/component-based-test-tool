using ExtensionPoints.ImplementedByContext;

namespace ExtensionPoints.ImplementedByComponents
{
  public interface TestComponentInstanceFactory
  {
    TestComponent Create();

    void AddTo(ComponentsList components);
  }
}