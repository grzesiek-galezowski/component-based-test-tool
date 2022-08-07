using Core.Maybe;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components.AzurePipelines;

public class AzurePipelinesComponent : ICoreTestComponent
{
  private Maybe<IOperationControl> _configureOperation;
  private Maybe<IOperationControl> _listPipelinesOperation;

  public void PopulateOperations(ITestComponentOperationDestination ctx)
  {
    ctx.AddOperation("Configure", _configureOperation.Value());
    ctx.AddOperation("List Pipelines", _listPipelinesOperation.Value());
  }

  public void CreateOperations(ITestComponentContext ctx)
  {
     _configureOperation = 
       ctx.CreateOperation(new ConfigureAzurePipelinesComponentInstanceOperation()).Just();
     _listPipelinesOperation = 
       ctx.CreateOperation(new ListPipelinesOperation()).Just();
  }
}