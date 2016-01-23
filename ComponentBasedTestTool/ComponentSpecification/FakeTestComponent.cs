using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using NSubstitute;

namespace ComponentSpecification
{
  public class FakeTestComponent
  {
    private readonly string _name;
    private readonly TestComponentInstanceFactory _factory;
    private readonly TestComponent _componentInstance;

    public FakeTestComponent(string name)
    {
      _name = name;
      _factory = Substitute.For<TestComponentInstanceFactory>();
      _componentInstance = Substitute.For<TestComponent>();
      _factory.Create().Returns(_componentInstance);
    }

    public void AddTo(ComponentsList components)
    {
      components.Add(_name, _factory);
    }
  }
}