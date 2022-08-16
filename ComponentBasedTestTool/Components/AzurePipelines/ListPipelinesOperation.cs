using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using Flurl.Http;
using Playground;

namespace Components.AzurePipelines;

public class ListPipelinesOperation : IComponentOperation
{
  public async Task RunAsync(CancellationToken token)
  {
    var organization = "grzesiekgalezowski";
    var project = "grzesiekgalezowski";

// LIST of pipelines request
    var jsonAsync = await $"https://dev.azure.com/{organization}/{project}/_apis/pipelines?api-version=7.1-preview.1"
      .GetJsonAsync<ListOfPipelines>();

    var pipelineIds = jsonAsync.Value.Select(p => p.Id);
    Console.WriteLine(string.Join(',', pipelineIds));
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