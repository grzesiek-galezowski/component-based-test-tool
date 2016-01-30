using System;
using System.Collections.Generic;
using System.Linq;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ComponentSpecification
{
  public class FakeComponentInstance : TestComponent
  {
    public string Name { get; }
    private readonly List<Tuple<string, FakeOperation>> _operations = new List<Tuple<string, FakeOperation>>();

    public FakeComponentInstance(string name)
    {
      Name = name;
    }

    public void PopulateOperations(TestComponentOperationDestination ctx)
    {
      foreach (var operation in _operations)
      {
        ctx.AddOperation(operation.Item1, operation.Item2);
      }
    }

    public void CreateOperations(TestComponentContext testComponentContext)
    {
      
    }

    public void ConfigureOperationWithName(string operationName)
    {
      _operations.Add(Tuple.Create(operationName, new FakeOperation()));
    }

    public void SetLastOperationParameter(string parameterName, string parameterValue)
    {
      _operations[_operations.Count - 1].Item2.AddParameter(parameterName, parameterValue);
    }

    public bool HasName(string instanceName)
    {
      return Name == instanceName;
    }

    public FakeOperation Get(string operationName)
    {
      return _operations.First(o => o.Item1 == operationName).Item2;
    }
  }
}