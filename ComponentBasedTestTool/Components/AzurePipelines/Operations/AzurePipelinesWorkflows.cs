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

public class PipelineRunSnapshot
{
  private readonly RunDto _run;
  private readonly AzurePipelinesRestApiClient _azurePipelinesRestApiClient;
  private readonly string _pipelineId;
  private readonly string _runId;

  public PipelineRunSnapshot(
    RunDto run,
    AzurePipelinesRestApiClient azurePipelinesRestApiClient,
    string pipelineId,
    string runId)
  {
    _run = run;
    _azurePipelinesRestApiClient = azurePipelinesRestApiClient;
    _pipelineId = pipelineId;
    _runId = runId;
  }

  public bool IsPipelineInProgress()
  {
    return _run.State != "completed";
  }

  public void SendTo(IPipelineObserver pipelineObserver)
  {
    pipelineObserver.NotifyAboutNew(_run);
  }

  public async Task<IEnumerable<LogPage>> DownloadLogs(CancellationToken token)
  {
    var logCollection = await _azurePipelinesRestApiClient.GetLogAsync(
      _pipelineId,
      _runId,
      token);
    var logPages = logCollection.Logs.Select(lp =>
      new LogPage(
        _azurePipelinesRestApiClient,
        _runId,
        _run,
        lp));
    return logPages;
  }
}