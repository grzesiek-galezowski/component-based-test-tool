using Flurl.Http;
using Playground;

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

//RUN pipeline
// requires sign in
var pipelineId = pipelineIds.First();
var runPipelineJson = await $"https://dev.azure.com/{organization}/{project}/_apis/pipelines/{pipelineId}/runs?api-version=7.1-preview.1"
  .WithHeader("Authorization", AuthorizationHeaderValue())
  .PostJsonAsync(new
  {
    previewRun = true
  });

var runPipelineResult = await runPipelineJson.GetJsonAsync<RunPipelineResult>();
Console.WriteLine(runPipelineResult);

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

  public record ReferenceLinks(Self Self, Web Web);
  public record Self(string Href);
  public record Web(string Href);

// run pipeline response


  public class RunPipelineResult
  {
    public RunPipelineLinks Links { get; set; }
    public Pipeline Pipeline { get; set; }
    public string State { get; set; }
    public string Url { get; set; }
    public string FinalYaml { get; set; }
    public int Id { get; set; }
    public object Name { get; set; }
  }

  public class RunPipelineLinks
  {
    public Self Self { get; set; }
    public Web Web { get; set; }
    public Web Pipelineweb { get; set; }
    public Web Pipeline { get; set; }
  }
}