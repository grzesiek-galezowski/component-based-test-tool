using Components.AzurePipelines.Client.Dto;
using Components.AzurePipelines.Operations;
using Flurl.Http;

namespace Components.AzurePipelines.Client;

public class AzurePipelinesRestApiClient
{
  private readonly string _tokenLocation;
  private readonly string _pipelinesApiPrefix;
  private readonly string _apiVersion;
  private string _apisPrefix;

  public AzurePipelinesRestApiClient(string organization, string project, string tokenLocation)
  {
    _tokenLocation = tokenLocation;
    _apisPrefix = $"https://dev.azure.com/{organization}/{project}/_apis";
    _pipelinesApiPrefix = $"{_apisPrefix}/pipelines";
    _apiVersion = "api-version=7.1-preview.1";
  }

  public async Task<LogCollection> GetLogAsync(string pipelineId, string runId, CancellationToken token)
  {
    return
      await AuthorizedPipelineApiRequest(pipelineId, $"/runs/{runId}/logs")
        .GetJsonAsync<LogCollection>(cancellationToken: token);
  }

  public async Task<string> GetLogPageDetailsAsync(
    string pipelineId,
    string runId,
    Log logEntry,
    CancellationToken token)
  {
    return await AuthorizedPipelineApiRequest(pipelineId, $"/runs/{runId}/logs/{logEntry.Id}")
      .GetStringAsync(cancellationToken: token);
  }

  public async Task<ListOfPipelinesDto> GetListOfPipelinesAsync(CancellationToken token)
  {
    return await ApiRequest("")
      .GetJsonAsync<ListOfPipelinesDto>(cancellationToken: token);
  }

  public async Task<RunDto> GetPipelineStatusAsync(string pipelineId, string runId, CancellationToken token)
  {
    return await
      AuthorizedPipelineApiRequest(pipelineId, $"/runs/{runId}")
        .GetJsonAsync<RunDto>(cancellationToken: token);
  }

  public async Task<RunDto> RunPipelineAsync(string pipelineId, CancellationToken token)
  {
    var runPipelineJson =
      await
        AuthorizedPipelineApiRequest(pipelineId, "/runs")
          .PostJsonAsync(new
          {
            previewRun = false
          }, cancellationToken: token);

    return await runPipelineJson.GetJsonAsync<RunDto>();
  }

  public async Task<string> GetLogPageContentAsync(string runId, int chapterId, CancellationToken token)
  {
    var url = $"{_apisPrefix}/build/builds/{runId}/logs/{chapterId}";
    return await url
      .GetStringAsync(cancellationToken: token);
  }

  private IFlurlRequest AuthorizedPipelineApiRequest(string pipelineId, string rest)
  {
    return AuthorizedApiRequest($"/{pipelineId}{rest}");
  }

  private IFlurlRequest AuthorizedApiRequest(string rest)
  {
    return ApiRequest(rest)
      .WithHeader("Authorization", AuthorizationHeader.Value(_tokenLocation));
  }

  private string ApiRequest(string rest)
  {
    return $"{_pipelinesApiPrefix}{rest}?{_apiVersion}";
  }
}