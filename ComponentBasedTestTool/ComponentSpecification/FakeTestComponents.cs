using System.Collections.Generic;
using System.Linq;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ComponentSpecification
{
  public class FakeTestComponents
  {
    private readonly List<FakeTestComponentSource> _components;
    private readonly List<FakeComponentInstance> _componentInstances;

    public FakeTestComponents()
    {
      _components = new List<FakeTestComponentSource>();
      _componentInstances = new List<FakeComponentInstance>();
    }

    public FakeTestComponentSource Add(string name)
    {
      var fakeComponentInstance = new FakeComponentInstance(name, "dummy description");
      var fakeTestComponentSource = new FakeTestComponentSource(fakeComponentInstance);
      _components.Add(fakeTestComponentSource);
      _componentInstances.Add(fakeComponentInstance);
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

    public FakeOperation RetrieveOperation(string instanceName, string operationName)
    {
      return _componentInstances.First(i => i.HasName(instanceName)).Get(operationName);
    }
  }
}