using System;
using System.Collections.Generic;
using Components.AzurePipelines.Client;
using Components.AzurePipelines.Client.Dto;
using ExtensionPoints.ImplementedByContext;

namespace Components.AzurePipelines.Operations;

public class PipelineLogsProgress
{
  public Dictionary<int, string> _returnValue;

  public PipelineLogsProgress(Dictionary<int, string> returnValue)
  {
    _returnValue = returnValue;
  }

  public bool AlreadyHasSomeLogsFor(Log logPageInfo)
  {
    return !_returnValue.ContainsKey(logPageInfo.Id);
  }

  public void Set(int id, string logContent)
  {
    _returnValue[id] = logContent;
  }

  public bool HasLessEntriesThanIn(string logContent, Log logPageInfo)
  {
    return logContent.Length > _returnValue[logPageInfo.Id].Length;
  }
}

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
    Run? runInfo;
    var pipelineLogsProgress = new PipelineLogsProgress(new Dictionary<int, string>());
    do
    {
      runInfo = await _azurePipelinesRestApiClient.GetPipelineStatusAsync(
        pipelineId,
        runId,
        token);

      pipelineObserver.NotifyAboutNew(runInfo);

      var logCollection = await _azurePipelinesRestApiClient.GetLogAsync(pipelineId, runId, token);

      foreach (var logPageInfo in logCollection.Logs)
      {
        //bug move this to log page and create a new type
        await Process(runId, runInfo, logPageInfo, pipelineObserver, pipelineLogsProgress, token);
      }
      await Task.Delay(TimeSpan.FromSeconds(20), token);
    } while (runInfo.State != "completed");
  }

  private async Task Process(string runId, Run runInfo, Log logPageInfo,
    IPipelineObserver pipelineObserver,
    PipelineLogsProgress pipelineLogsProgress, CancellationToken token)
  {
    var logContent = await _azurePipelinesRestApiClient.GetLogPageContentAsync(runId, logPageInfo.Id, token);

    //bug there may be some races here...
    if (pipelineLogsProgress.AlreadyHasSomeLogsFor(logPageInfo))
    {
      pipelineLogsProgress.Set(logPageInfo.Id, logContent);
      pipelineObserver.NotifyAboutLogs(runInfo, logPageInfo, logContent);
    }
    else
    {
      if (pipelineLogsProgress.HasLessEntriesThanIn(logContent, logPageInfo))
      {
        pipelineObserver.NotifyAboutLogs(
          runInfo,
          logPageInfo,
          OnlyNewEntriesIn(logContent, pipelineLogsProgress, logPageInfo));
      }
    }
  }

  private static string OnlyNewEntriesIn(string logContent, PipelineLogsProgress pipelineLogsProgress, Log logPageInfo)
  {
    return logContent[(pipelineLogsProgress._returnValue[logPageInfo.Id].Length-1)..];
  }
}