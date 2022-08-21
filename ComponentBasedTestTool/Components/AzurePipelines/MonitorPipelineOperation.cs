using Components.AzurePipelines.Dto;
using Core.Maybe;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using Flurl.Http;
using System;

namespace Components.AzurePipelines;

public class MonitorPipelineOperation : IComponentOperation
{
  private readonly IOperationsOutput _out;
  private readonly AzurePipelinesComponentConfiguration _config;
  private Maybe<IOperationParameter<string>> _idParam;
  private Maybe<IOperationParameter<string>> _runIdParam;

  public MonitorPipelineOperation(IOperationsOutput @out,
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

    Run? runInfo;
    do
    {
      // GET run status
      runInfo = await
        $"https://dev.azure.com/{organization}/{project}/_apis/pipelines/{_idParam.Value().Value}/runs/{_runIdParam.Value().Value}?api-version=6.0-preview.1"
          .WithHeader("Authorization", AuthorizationHeader.Value(tokenLocation))
          .GetJsonAsync<Run>(cancellationToken: token);

      _out.WriteLine(runInfo.ToString());
      await Task.Delay(TimeSpan.FromSeconds(20), token);

    } while (runInfo.State != "completed");
  }

  public void InitializeParameters(IOperationParametersListBuilder parameters)
  {
    _idParam = parameters.Text("Pipeline Id", "1").Just(); //bug Add parameter type int
    _runIdParam = parameters.Text("Run Id", "").Just(); //bug Add parameter type int
  }

  public void StoreParameters(IPersistentStorage destination)
  {
    //bug
  }
}