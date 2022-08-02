using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using NSubstitute;

namespace ComponentSpecification.AutomationLayer;

public class FakeOperation : IComponentOperation
{
  private readonly List<Tuple<string, string>> _parameters = new();
  private readonly IRunnable _mock = Substitute.For<IComponentOperation>();

  public Task RunAsync(CancellationToken token)
  {
    return _mock.RunAsync(token);
  }

  public FakeOperation AddParameter(string name, string defaultValue)
  {
    _parameters.Add(Tuple.Create(name, defaultValue));
    return this;
  }

  public void InitializeParameters(IOperationParametersListBuilder parameters)
  {
    foreach (var parameter in _parameters)
    {
      parameters.Text(parameter.Item1, parameter.Item2);
    }
  }

  public void StoreParameters(IPersistentStorage destination)
  {
      
  }

  public void StoreParameters(ITestComponentOperationDestination destination)
  {
      
  }

  public void AssertWasRun(int count)
  {
    _mock.Received(count).RunAsync(Arg.Any<CancellationToken>());
  }
}