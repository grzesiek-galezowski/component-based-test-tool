using ExtensionPoints.ImplementedByComponents;

namespace ExtensionPoints.ImplementedByContext
{
  public interface ComponentsList
  {
    void Add(string name, string description, TestComponentInstanceFactory factory);
  }
}