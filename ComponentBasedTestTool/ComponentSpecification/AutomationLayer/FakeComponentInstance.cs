using System;
using System.Linq;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
using Xunit;

namespace ComponentSpecification.AutomationLayer;

public class FakeComponentInstance : CoreTestComponent, Capabilities.All
{
  public string Name { get; }
  public string Description { get; }

  private readonly List<Tuple<string, FakeOperation>> _operations = new();
  private readonly List<Tuple<string, OperationControl>> _realOperations = new();
  private int _customUiCallsCount = 0;
  private int _cleanupOnClosingCallCount = 0;

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

  public void CreateOperations(TestComponentContext ctx)
  {
    var operationStateMachines = 
      _operations.Select(o => Tuple.Create(o.Item1, ctx.CreateOperation(o.Item2))).ToList();
    _realOperations.AddRange(operationStateMachines);
  }

  public void ShowCustomUi()
  {
    _customUiCallsCount++;
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
    return instanceName.StartsWith(Name);
  }

  public FakeOperation Get(string operationName)
  {
    return _operations.First(o => o.Item1 == operationName).Item2;
  }

  public void CleanupOnClosing()
  {
    _cleanupOnClosingCallCount++;
  }

  public void AssertReceivedACommandToShowCustomUi()
  {
    Assert.Equal(1, _customUiCallsCount);
  }

  public void AssertReceivedClosingNotification()
  {
    Assert.Equal(1, _cleanupOnClosingCallCount);
  }
}