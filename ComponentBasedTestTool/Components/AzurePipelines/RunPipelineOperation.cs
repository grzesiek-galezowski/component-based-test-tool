using Components.AzurePipelines.Dto;
using Core.Maybe;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using Flurl.Http;

namespace Components.AzurePipelines;

public class RunPipelineOperation : IComponentOperation
{
  private readonly IOperationsOutput _out;
  private readonly AzurePipelinesComponentConfiguration _config;
  private Maybe<IOperationParameter<string>> _idParam;

  public RunPipelineOperation(IOperationsOutput @out,
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

    //RUN pipeline
    // requires sign in
    var runPipelineJson =
      await
        $"https://dev.azure.com/{organization}/{project}/_apis/pipelines/{_idParam.Value()}/runs?api-version=7.1-preview.1"
          .WithHeader("Authorization", AuthorizationHeader.Value(tokenLocation))
          .PostJsonAsync(new
          {
            previewRun = false
          }, cancellationToken: token);

    var runPipelineResult = await runPipelineJson.GetJsonAsync<Run>();
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