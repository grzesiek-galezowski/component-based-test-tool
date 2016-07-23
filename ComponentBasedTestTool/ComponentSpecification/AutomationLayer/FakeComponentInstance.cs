using System;
using System.Collections.Generic;
using System.Linq;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ComponentSpecification.AutomationLayer
{
  public class FakeComponentInstance : TestComponent
  {
    public string Name { get; }
    public string Description { get; }

    private readonly List<Tuple<string, FakeOperation>> _operations = new List<Tuple<string, FakeOperation>>();
    private readonly List<Tuple<string, OperationStateMachine>> _realOperations = new List<Tuple<string, OperationStateMachine>>();

    public FakeComponentInstance(string name, string description)
    {
      Name = name;
      Description = description;
    }

    public void PopulateOperations(TestComponentOperationDestination ctx)
    {
      foreach (var operation in _realOperations)
      {
        ctx.AddOperation(operation.Item1, operation.Item2);
      }
    }

    public void CreateOperations(TestComponentContext testComponentContext)
    {
      var operationStateMachines = 
        _operations.Select(o => Tuple.Create(o.Item1, testComponentContext.CreateOperation(o.Item2))).ToList();
      _realOperations.AddRange(operationStateMachines);
    }

    public void ShowCustomUi()
    {
      throw new NotImplementedException();
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