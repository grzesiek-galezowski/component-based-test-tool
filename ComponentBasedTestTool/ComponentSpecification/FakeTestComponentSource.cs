using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using NSubstitute;

namespace ComponentSpecification
{
  public class FakeTestComponentSource
  {
    private readonly string _name;
    private readonly TestComponentInstanceFactory _factory;
    private readonly FakeComponentInstance _componentInstance;
    private string _description;

    public FakeTestComponentSource(FakeComponentInstance fakeComponentInstance)
    {
      _name = fakeComponentInstance.Name;
      _description = fakeComponentInstance.Description;
      _factory = Substitute.For<TestComponentInstanceFactory>();
      _componentInstance = fakeComponentInstance;
      _factory.Create().Returns(_componentInstance);
    }

    public void AddTo(ComponentsList components)
    {
      components.Add(_name, _description, _factory);
    }

    public FakeTestComponentSource WithOperation(string operationName)
    {
      _componentInstance.ConfigureOperationWithName(operationName);
      return this;
    }

    public FakeTestComponentSource WithParameter(string parameterName, string parameterValue)
    {
      _componentInstance.SetLastOperationParameter(parameterName, parameterValue);
      return this;
    }
  }
}