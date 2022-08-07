using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Maybe;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components.AzurePipelines;

public class ConfigureAzurePipelinesComponentInstanceOperation : IComponentOperation
{
  private Maybe<IOperationParameter<string>> _tokenLocation;
  private Maybe<IOperationParameter<string>> _organization;
  private Maybe<IOperationParameter<string>> _project;

  public async Task RunAsync(CancellationToken token)
  {
    throw new NotImplementedException();
  }

  public void InitializeParameters(IOperationParametersListBuilder parameters)
  {
    _tokenLocation 
      = parameters.Path("Personal token location", "C:\\Users\\HYPERBOOK\\Documents\\__KEYS\\azure-pipelines.txt").Just();
    _organization = parameters.Text("Organization", "grzesiekgalezowski").Just();
    _project = parameters.Text("Project", "grzesiekgalezowski").Just();
  }

  public void StoreParameters(IPersistentStorage destination)
  {
    //bug TODO
  }
}