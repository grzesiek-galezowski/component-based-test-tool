using ExtensionPoints.ImplementedByComponents;

namespace ExtensionPoints.ImplementedByContext;

public interface IComponentsList
{
  void Add(string name, string description, ITestComponentInstanceFactory factory);
}