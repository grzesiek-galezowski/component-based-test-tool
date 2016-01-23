using System.Collections.Generic;
using ExtensionPoints.ImplementedByContext;

namespace ComponentSpecification
{
  public class FakeTestComponents
  {
    private readonly List<FakeTestComponent> _components = new List<FakeTestComponent>();

    public void Add(string name)
    {
      _components.Add(new FakeTestComponent(name));
    }

    public void AddTo(ComponentsList componentsList)
    {
      foreach (var fakeTestComponent in _components)
      {
        fakeTestComponent.AddTo(componentsList);
      }
    }
  }
}