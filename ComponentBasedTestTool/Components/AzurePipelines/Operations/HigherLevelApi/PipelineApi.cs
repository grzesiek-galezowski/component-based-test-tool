using Components.AzurePipelines.Client;

namespace Components.AzurePipelines.Operations.HigherLevelApi;

public class PipelineApi
{
  public readonly AzurePipelinesRestApiClient _azurePipelinesRestApiClient;

  public PipelineApi(AzurePipelinesRestApiClient azurePipelinesRestApiClient)
  {
    _azurePipelinesRestApiClient = azurePipelinesRestApiClient;
  }

  public async Task<PipelineRunSnapshot> GetRunningPipelineAsync(string pipelineId, string runId, CancellationToken token)
  {
    return new PipelineRunSnapshot(
      await _azurePipelinesRestApiClient.GetPipelineStatusAsync(
        pipelineId,
        runId,
        token),
      _azurePipelinesRestApiClient,
      pipelineId,
      runId);
  }
}