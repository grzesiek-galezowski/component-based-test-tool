using System.Collections.Generic;
using Components.AzurePipelines.Client;
using Components.AzurePipelines.Client.Dto;

namespace Components.AzurePipelines.Operations.HigherLevelApi;

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