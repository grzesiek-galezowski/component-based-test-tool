using System.Collections.Generic;
using ExtensionPoints.ImplementedByContext;

namespace ComponentSpecification
{
  public class FakeTestComponents
  {
    private readonly List<FakeTestComponentSource> _components = new List<FakeTestComponentSource>();

    public FakeTestComponentSource Add(string name)
    {
      var fakeTestComponentSource = new FakeTestComponentSource(name);
      _components.Add(fakeTestComponentSource);
      return fakeTestComponentSource;
    }

    public void AddTo(ComponentsList componentsList)
    {
      foreach (var fakeTestComponent in _components)
      {
        fakeTestComponent.AddTo(componentsList);
      }
    }

    public void AddWithNames(params string[] names)
    {
      foreach (var name in names)
      {
        Add(name);
      }
    }
  }
}