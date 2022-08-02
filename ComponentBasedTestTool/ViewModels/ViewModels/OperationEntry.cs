using ComponentBasedTestTool.Domain;
using Core.Maybe;
using ViewModels.Composition;

namespace ViewModels.ViewModels;

public class OperationEntry
{
  public string ParentComponentInstanceName { get; }
  public Maybe<string> DependencyName { get; }
  public string Name { get; }
  private IOperationStateMachine InnerOperation { get; }

  public OperationEntry(
    string parentComponentInstanceName, 
    string name, 
    IOperationStateMachine innerOperation, 
    Maybe<string> dependencyName,
    IOperationStateMachine operationStateMachine)
  {
    ParentComponentInstanceName = parentComponentInstanceName;
    Name = name;
    InnerOperation = innerOperation;
    DependencyName = dependencyName;
    OperationStateMachine = operationStateMachine;
  }

  public static OperationEntry With(string componentInstanceName, string name, IOperationStateMachine operation, Maybe<string> dependencyName, IOperationStateMachine operationStateMachine)
  {
    return new OperationEntry(componentInstanceName, name, operation, dependencyName, operationStateMachine);
  }

  public void AddParametersTo(OperationPropertiesViewModelBuilder operationPropertiesViewModelBuilder)
  {
    InnerOperation.InitializeParametersIn(operationPropertiesViewModelBuilder);
  }

  //ugly: unclear role of this class
  public IOperationStateMachine OperationStateMachine { get; }

  public OperationViewModel ToOperationViewModel(IOperationViewModelFactory operationViewModelFactory)
  {
    var operationViewModel = 
      operationViewModelFactory.CreateOperationViewModel(this, OperationStateMachine);
    return operationViewModel;
  }
}