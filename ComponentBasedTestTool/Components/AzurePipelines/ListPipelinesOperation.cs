using System;
using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components.AzurePipelines;

public class ListPipelinesOperation : IComponentOperation
{
  public async Task RunAsync(CancellationToken token)
  {
    throw new NotImplementedException();
  }

  public void InitializeParameters(IOperationParametersListBuilder parameters)
  {
    throw new NotImplementedException();
  }

  public void StoreParameters(IPersistentStorage destination)
  {
    throw new NotImplementedException();
  }
}