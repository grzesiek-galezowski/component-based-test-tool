using CallMeMaybe;
using ComponentBasedTestTool.Domain;
using ViewModels.Composition;

namespace ViewModels.ViewModels
{
  public class OperationEntry
  {
    public string ParentComponentInstanceName { get; }
    public Maybe<string> DependencyName { get; }
    public string Name { get; }
    private OperationStateMachine InnerOperation { get; }

    public OperationEntry(
      string parentComponentInstanceName, 
      string name, 
      OperationStateMachine innerOperation, 
      Maybe<string> dependencyName,
      OperationStateMachine operationStateMachine)
    {
      ParentComponentInstanceName = parentComponentInstanceName;
      Name = name;
      InnerOperation = innerOperation;
      DependencyName = dependencyName;
      OperationStateMachine = operationStateMachine;
    }

    public static OperationEntry With(string componentInstanceName, string name, OperationStateMachine operation, Maybe<string> dependencyName, OperationStateMachine operationStateMachine)
    {
      return new OperationEntry(componentInstanceName, name, operation, dependencyName, operationStateMachine);
    }

    public void AddParametersTo(OperationPropertiesViewModelBuilder operationPropertiesViewModelBuilder)
    {
      InnerOperation.InitializeParametersIn(operationPropertiesViewModelBuilder);
    }

    //ugly: unclear role of this class
    public OperationStateMachine OperationStateMachine { get; }

    public OperationViewModel ToOperationViewModel(OperationViewModelFactory operationViewModelFactory)
    {
      var operationViewModel = 
        operationViewModelFactory.CreateOperationViewModel(this, OperationStateMachine);
      return operationViewModel;
    }
  }
}