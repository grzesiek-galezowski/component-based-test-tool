using Components.AzurePipelines.Client;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components.AzurePipelines.Operations;

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

    var azurePipelinesClient = new AzurePipelinesRestApiClient(
      organization.Value(),
      project.Value(),
      _config.TokenLocation.Value());
    var jsonAsync = await azurePipelinesClient.GetListOfPipelinesAsync(token);

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