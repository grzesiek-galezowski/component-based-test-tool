using Components.AzurePipelines.Dto;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using Flurl.Http;

namespace Components.AzurePipelines;

public class ListPipelinesOperation : IComponentOperation
{
  private readonly IOperationsOutput _out;
  private readonly AzurePipelinesComponentConfiguration _config;

  public ListPipelinesOperation(IOperationsOutput @out,
    AzurePipelinesComponentConfiguration config)
  {
    _out = @out;
    _config = config;
  }

  public async Task RunAsync(CancellationToken token)
  {
    var organization = _config.Organization;
    var project = _config.Project;

    var jsonAsync = await $"https://dev.azure.com/{organization}/{project}/_apis/pipelines?api-version=7.1-preview.1"
      .GetJsonAsync<ListOfPipelines>(cancellationToken: token);

    _out.WriteLine(string.Join(',', jsonAsync.Value.Select(v => v.ToString())));
  }

  public void InitializeParameters(IOperationParametersListBuilder parameters)
  {
  }

  public void StoreParameters(IPersistentStorage destination)
  {
    //bug
  }
}