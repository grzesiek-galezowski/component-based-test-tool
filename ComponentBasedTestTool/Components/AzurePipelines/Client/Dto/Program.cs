using System;
using System.IO;
using Flurl.Http;

namespace Components.AzurePipelines.Client.Dto;

public class Kike
{
  public static async Task Kike2()
  {
    string AuthorizationHeaderValue()
    {
      var personalAccessToken = File.ReadAllText("C:\\Users\\HYPERBOOK\\Documents\\__KEYS\\azure-pipelines.txt");
      var base64String = Convert.ToBase64String(
        System.Text.Encoding.ASCII.GetBytes(
          $":{personalAccessToken}"));
      return $"Basic {base64String}";
    }

    var organization = "grzesiekgalezowski";
    var project = "grzesiekgalezowski";

    // LIST of pipelines request
    var jsonAsync = await $"https://dev.azure.com/{organization}/{project}/_apis/pipelines?api-version=7.1-preview.1"
      .GetJsonAsync<ListOfPipelines>();

    var pipelineIds = jsonAsync.Value.Select(p => p.Id);
    Console.WriteLine(string.Join(',', pipelineIds));
    var pipelineId = pipelineIds.First();

    //GET pipeline info

    var str =
      await $"https://dev.azure.com/{organization}/{project}/_apis/pipelines/{pipelineId}?api-version=6.0-preview.1"
        .WithHeader("Authorization", AuthorizationHeaderValue())
        .GetStringAsync();

    Console.WriteLine(str);

    //RUN pipeline
    // requires sign in
    var runPipelineJson =
      await
        $"https://dev.azure.com/{organization}/{project}/_apis/pipelines/{pipelineId}/runs?api-version=7.1-preview.1"
          .WithHeader("Authorization", AuthorizationHeaderValue())
          .PostJsonAsync(new
          {
            previewRun = false
          });

    var runPipelineResult = await runPipelineJson.GetJsonAsync<Run>();
    Console.WriteLine(runPipelineResult);

    Run? runInfo;
    do
    {
      // GET run status
      runInfo = await
        $"https://dev.azure.com/{organization}/{project}/_apis/pipelines/{pipelineId}/runs/{runPipelineResult.Id}?api-version=6.0-preview.1"
          .WithHeader("Authorization", AuthorizationHeaderValue())
          .GetJsonAsync<Run>();


      Console.WriteLine(runInfo.State);
      await Task.Delay(TimeSpan.FromSeconds(20));

    } while (runInfo.State != "completed");

  }
}