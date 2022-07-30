using System.Linq;
using ExtensionPoints.ImplementedByContext;

namespace ComponentSpecification.AutomationLayer
{
  public class FakeTestComponents
  {
    private readonly List<FakeTestComponentSource> _componentSources;
    private readonly List<FakeComponentInstance> _componentInstances;

    public FakeTestComponents(List<FakeComponentInstance> fakeComponentInstances)
    {
      _componentSources = new List<FakeTestComponentSource>();
      _componentInstances = fakeComponentInstances;
    }

    public FakeTestComponentSource Add(string name)
    {
      var fakeComponentInstance = new FakeComponentInstance(name, "dummy description");
      var fakeTestComponentSource = new FakeTestComponentSource(fakeComponentInstance);
      _componentSources.Add(fakeTestComponentSource);
      _componentInstances.Add(fakeComponentInstance);
      return fakeTestComponentSource;
    }

    public void AddTo(ComponentsList componentsList)
    {
      foreach (var fakeTestComponent in _componentSources)
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