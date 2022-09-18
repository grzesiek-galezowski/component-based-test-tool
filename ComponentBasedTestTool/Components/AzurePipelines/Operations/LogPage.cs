using Components.AzurePipelines.Client;
using Components.AzurePipelines.Client.Dto;

namespace Components.AzurePipelines.Operations;

public class LogPage
{
  private readonly Log _logPageInfo;
  private readonly RunDto _runInfo;
  private readonly string _runId;
  private readonly AzurePipelinesRestApiClient _azurePipelinesRestApiClient;

  public LogPage(
    AzurePipelinesRestApiClient azurePipelinesRestApiClient,
    string runId,
    RunDto runInfo,
    Log logPageInfo)
  {
    _azurePipelinesRestApiClient = azurePipelinesRestApiClient;
    _runId = runId;
    _runInfo = runInfo;
    _logPageInfo = logPageInfo;
  }

  public async Task Process(IPipelineObserver pipelineObserver, PipelineLogsProgress pipelineLogsProgress,
    CancellationToken cancellationToken)
  {
    var logContent =
      await _azurePipelinesRestApiClient.GetLogPageContentAsync(
        _runId,
        _logPageInfo.Id,
        cancellationToken);

    //bug there may be some races here...
    if (pipelineLogsProgress.AlreadyHasSomeLogsFor(_logPageInfo))
    {
      pipelineLogsProgress.Set(_logPageInfo.Id, logContent);
      pipelineObserver.NotifyAboutLogs(_runInfo, _logPageInfo, logContent);
    }
    else
    {
      if (pipelineLogsProgress.HasLessEntriesThanIn(logContent, _logPageInfo))
      {
        pipelineObserver.NotifyAboutLogs(
          _runInfo,
          _logPageInfo,
          OnlyNewEntriesIn(logContent, pipelineLogsProgress, _logPageInfo));
      }
    }
  }

  private static string OnlyNewEntriesIn(string logContent, PipelineLogsProgress pipelineLogsProgress, Log logPageInfo)
  {
    return logContent[(pipelineLogsProgress._returnValue[logPageInfo.Id].Length-1)..];
  }
}