using System;
using Components.AzurePipelines.Client;
using Components.AzurePipelines.Client.Dto;
using ExtensionPoints.ImplementedByContext;

namespace Components.AzurePipelines.Operations;

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
    var log = await _azurePipelinesRestApiClient.GetLogAsync(token, pipelineId, runId);

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

  public async Task MonitorBuild(
    IOperationsOutput operationsOutput,
    string pipelineId,
    string runId,
    CancellationToken token)
  {
    Run? runInfo;
    do
    {
      runInfo = await _azurePipelinesRestApiClient.GetPipelineStatusAsync(
        pipelineId,
        runId,
        token);

      //bug add logs

      operationsOutput.WriteLine(runInfo.ToString());
      await Task.Delay(TimeSpan.FromSeconds(20), token);
    } while (runInfo.State != "completed");
  }
}