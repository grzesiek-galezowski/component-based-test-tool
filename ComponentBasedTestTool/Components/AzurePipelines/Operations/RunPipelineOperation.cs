using Components.AzurePipelines.Client;
using Core.Maybe;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components.AzurePipelines.Operations;

public class RunPipelineOperation : IComponentOperation
{
  private readonly IOperationsOutput _out;
  private readonly AzurePipelinesComponentConfiguration _config;
  private Maybe<IOperationParameter<string>> _idParam;

  public RunPipelineOperation(
    IOperationsOutput @out,
    AzurePipelinesComponentConfiguration config)
  {
    _out = @out;
    _config = config;
  }

  public async Task RunAsync(CancellationToken token)
  {
    var organization = _config.Organization.Value();
    var project = _config.Project.Value();
    var tokenLocation = _config.TokenLocation.Value();

    var azurePipelinesClient = new AzurePipelinesRestApiClient(organization, project, tokenLocation);
    var runPipelineResult = await azurePipelinesClient.RunPipelineAsync(
      _idParam.Value().Value,
      token);
    _out.WriteLine(runPipelineResult.ToString());
  }

  public void InitializeParameters(IOperationParametersListBuilder parameters)
  {
    _idParam = parameters.Text("Id", "1").Just(); //bug Add parameter type int
  }

  public void StoreParameters(IPersistentStorage destination)
  {
    //bug
  }
}