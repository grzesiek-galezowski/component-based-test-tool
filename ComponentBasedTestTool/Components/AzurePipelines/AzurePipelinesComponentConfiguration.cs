using Core.Maybe;

namespace Components.AzurePipelines;

public class AzurePipelinesComponentConfiguration
{
  public Maybe<string> TokenLocation { get; set; }
  public Maybe<string> Organization { get; set; }
  public Maybe<string> Project { get; set; }
}