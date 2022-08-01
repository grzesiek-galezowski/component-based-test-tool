using Flurl.Http;

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

//List of pipelines DTO
public record ListOfPipelines(int Count, Pipeline[] Value);

public record Pipeline(
  ReferenceLinks _Links,
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
  public RunPipelineLinks _links { get; set; }
  public Pipeline pipeline { get; set; }
  public string state { get; set; }
  public string url { get; set; }
  public string finalYaml { get; set; }
  public int id { get; set; }
  public object name { get; set; }
}

public class RunPipelineLinks
{
  public Self self { get; set; }
  public Web web { get; set; }
  public Web pipelineweb { get; set; }
  public Web pipeline { get; set; }
}
