using CallMeMaybe;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ViewModels.ViewModels
{
  public class OperationEntry
  {
    private readonly OperationStateMachine _operation;
    public Maybe<string> DependencyName { get; }
    public string Name { get; }
    private OperationStateMachine InnerOperation { get; } //TODO get rid of this. Only state machine should stay!!!

    public OperationEntry(string name, OperationStateMachine innerOperation, Maybe<string> dependencyName, OperationStateMachine operationStateMachine)
    {
      Name = name;
      InnerOperation = innerOperation;
      DependencyName = dependencyName;
      _operation = operationStateMachine;
    }

    public static OperationEntry With(string name, OperationStateMachine operation, Maybe<string> dependencyName, OperationStateMachine operationStateMachine)
    {
      return new OperationEntry(name, operation, dependencyName, operationStateMachine);
    }

    public void FillParameters(OperationPropertiesViewModelBuilder operationPropertiesViewModelBuilder)
    {
      InnerOperation.InitializeParameters(operationPropertiesViewModelBuilder);
    }

    public OperationStateMachine OperationStateMachine
    {
      get
      {
        return _operation;
      }
    }
  }
}