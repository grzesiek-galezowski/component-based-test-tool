using System;
using System.Collections.Generic;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ComponentSpecification
{
  public class FakeComponentInstance : TestComponent
  {
    private readonly List<Tuple<string, FakeOperation>> _operations = new List<Tuple<string, FakeOperation>>();

    public void PopulateOperations(TestComponentContext ctx)
    {
      foreach (var operation in _operations)
      {
        ctx.AddOperation(operation.Item1, operation.Item2);
      }
    }

    public void ConfigureOperationWithName(string operationName)
    {
      _operations.Add(Tuple.Create(operationName, new FakeOperation()));
    }

    public void SetLastOperationParameter(string parameterName, string parameterValue)
    {
      _operations[_operations.Count - 1].Item2.AddParameter(parameterName, parameterValue);
    }
  }
}