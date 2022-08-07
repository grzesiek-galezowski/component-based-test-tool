using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using Playground;

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

public class Resources
{
  public Repositories repositories { get; set; }
}

public class Repositories
{
  public Self1 self { get; set; }
}

public class Self1
{
  public Repository repository { get; set; }
  public string refName { get; set; }
  public string version { get; set; }
}

public class Repository
{
  public string fullName { get; set; }
  public Connection connection { get; set; }
  public string type { get; set; }
}

public class Connection
{
  public string id { get; set; }
}

namespace Playground
{
  //List of pipelines DTO
  public record ListOfPipelines(int Count, Pipeline[] Value);

  public record Pipeline(
    ReferenceLinks Links,
    string Url,
    int Id,
    int Revision,
    string Name,
    string Folder
  );

  public record Self(string Href);
  public record Web(string Href);

  // run pipeline response

  public record Run
  (
    ReferenceLinks Links,
    Pipeline Pipeline,
    string State,
    DateTime CreatedDate,
    string Url,
    Resources Resources,
    string FinalYaml,
    int Id,
    object Name
  );

  public record ReferenceLinks(
    Self Self,
    Web Web,
    Web Pipelineweb,
    Web Pipeline
  );
}
