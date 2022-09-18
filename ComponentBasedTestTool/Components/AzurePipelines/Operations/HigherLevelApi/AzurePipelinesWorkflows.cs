using System;
using System.Collections.Generic;
using Components.AzurePipelines.Client;
using ExtensionPoints.ImplementedByContext;

namespace Components.AzurePipelines.Operations.HigherLevelApi;

public class AzurePipelinesWorkflows
{
  private readonly AzurePipelinesRestApiClient _azurePipelinesRestApiClient;

  public AzurePipelinesWorkflows(string organization, string project, string tokenLocation)
  {
    _azurePipelinesRestApiClient =
      new AzurePipelinesRestApiClient(organization, project, tokenLocation);
  }

  public async Task PrintAllLogs(
    IOperationsOutput operationsOutput,
    string pipelineId,
    string runId,
    CancellationToken token)
  {
    var log = await _azurePipelinesRestApiClient.GetLogAsync(pipelineId, runId, token);

    operationsOutput.WriteLine("======== Log pages ====== ");
    operationsOutput.WriteLine(log.ToString());

    foreach (var logPageInfo in log.Logs)
    {
      var logJson =
        await _azurePipelinesRestApiClient.GetLogPageDetailsAsync(pipelineId, runId, logPageInfo, token);
      operationsOutput.WriteLine("======== Log page details ====== ");
      operationsOutput.WriteLine(logJson);

      operationsOutput.WriteLine("======== Log page content ====== ");
      var logContent = await _azurePipelinesRestApiClient.GetLogPageContentAsync(runId, logPageInfo.Id, token);
      operationsOutput.WriteLine(logContent);
    }
  }

  public async Task MonitorBuild(string pipelineId,
    string runId,
    CancellationToken token,
    IPipelineObserver pipelineObserver)
  {
    //bug unfinished. Finish refactoring and test this through the tool
    bool isPipelineInProgress;
    var pipelineLogsProgress = new PipelineLogsProgress(new Dictionary<int, string>());
    var pipelineApi = new PipelineApi(_azurePipelinesRestApiClient);
    do
    {
      var pipelineRunSnapshot = await pipelineApi.GetRunningPipelineAsync(pipelineId, runId, token);
      pipelineRunSnapshot.SendTo(pipelineObserver);

      var logPages = await pipelineRunSnapshot.DownloadLogs(token);

      foreach (var logPage in logPages)
      {
        await logPage.Process(pipelineObserver, pipelineLogsProgress, token);
      }
      await Task.Delay(TimeSpan.FromSeconds(20), token);

      isPipelineInProgress = pipelineRunSnapshot.IsPipelineInProgress();
    } while (isPipelineInProgress);
  }
}