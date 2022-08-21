using System;
using System.Security.Cryptography;
using Components.AzurePipelines.Dto;
using Core.Maybe;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using Flurl.Http;

namespace Components.AzurePipelines;

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
      var logs = await
        $"https://dev.azure.com/{organization}/{project}/_apis/pipelines/{pipelineId}/runs/{runId}/logs?api-version=7.1-preview.1"
          .WithHeader("Authorization", AuthorizationHeader.Value(tokenLocation))
          .GetJsonAsync<LogCollection>(cancellationToken: token);

      _out.WriteLine(logs.ToString());

      foreach (var logEntry in logs.Logs)
      {
        var logJson =
          await $"https://dev.azure.com/{organization}/{project}/_apis/pipelines/{pipelineId}/runs/{runId}/logs/{logEntry.Id}?api-version=7.1-preview.1"
            .WithHeader("Authorization", AuthorizationHeader.Value(tokenLocation))
            .GetStringAsync(cancellationToken: token);
        _out.WriteLine(logJson);

        var logContent = await $"https://dev.azure.com/{organization}/{project}/_apis/build/builds/{runId}/logs/{logEntry.Id}"
          .WithHeader("Authorization", AuthorizationHeader.Value(tokenLocation))
          .GetStringAsync(cancellationToken: token);
        _out.WriteLine(logContent);
      }
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

public record Log(DateTime CreatedOn, int Id, DateTime LastChangedOn, int LineCount, string Url);
