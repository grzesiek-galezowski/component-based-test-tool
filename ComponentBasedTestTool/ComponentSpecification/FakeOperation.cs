using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ComponentSpecification
{
  public class FakeOperation : Operation
  {
    private readonly List<Tuple<string, string>> _parameters = new List<Tuple<string, string>>();

    public Task RunAsync(CancellationToken token)
    {
      return Task.CompletedTask;
    }

    public FakeOperation AddParameter(string name, string defaultValue)
    {
      _parameters.Add(Tuple.Create(name, defaultValue));
      return this;
    }

    public void FillParameters(OperationParametersListBuilder parameters)
    {
      foreach (var parameter in _parameters)
      {
        parameters.Text(parameter.Item1, parameter.Item2);
      }
    }
  }
}