using ExtensionPoints.ImplementedByContext;

namespace ExtensionPoints.ImplementedByComponents;

public interface ITestComponentSourceRoot
{
  void AddTo(IComponentsList components);
}