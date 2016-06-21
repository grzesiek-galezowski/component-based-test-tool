using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using NSubstitute;

namespace ComponentSpecification
{
  public class FakeOperation : ComponentOperation
  {
    private readonly List<Tuple<string, string>> _parameters = new List<Tuple<string, string>>();
    private readonly Runnable _mock = Substitute.For<ComponentOperation>();

    public Task RunAsync(CancellationToken token)
    {
      return _mock.RunAsync(token);
    }

    public FakeOperation AddParameter(string name, string defaultValue)
    {
      _parameters.Add(Tuple.Create(name, defaultValue));
      return this;
    }

    public void InitializeParameters(OperationParametersListBuilder parameters)
    {
      foreach (var parameter in _parameters)
      {
        parameters.Text(parameter.Item1, parameter.Item2);
      }
    }

    public void StoreParameters(PersistentStorage destination)
    {
      
    }

    public void StoreParameters(TestComponentOperationDestination destination)
    {
      
    }

    public void AssertWasRun()
    {
      _mock.Received(1).RunAsync(Arg.Any<CancellationToken>());
    }
  }
}