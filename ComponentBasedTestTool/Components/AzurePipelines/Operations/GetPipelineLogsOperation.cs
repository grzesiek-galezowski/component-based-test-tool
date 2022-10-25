using System;
using Components.AzurePipelines.Operations.HigherLevelApi;
using Core.Maybe;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components.AzurePipelines.Operations;

public class GetPipelineLogsOperation : IComponentOperation
{
  private readonly IOperationsOutput _out;
  private readonly AzurePipelinesComponentConfiguration _config;
  private Maybe<IOperationParameter<string>> _idParam;
  private Maybe<IOperationParameter<string>> _runIdParam;

  public GetPipelineLogsOperation(IOperationsOutput @out,
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

    // GET run status
    var pipelineId = _idParam.Value().Value;
    var runId = _runIdParam.Value().Value;
    var azurePipelinesWorkflows = new AzurePipelinesWorkflows(
      organization, 
      project, 
      tokenLocation);
    await azurePipelinesWorkflows.PrintAllLogs(_out, pipelineId, runId, token);
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

public record LogCollection(Log[] Logs, string Url);

public record Log(DateTime CreatedOn, int Id, DateTime LastChangedOn, int LineCount, string Url)
{
}
