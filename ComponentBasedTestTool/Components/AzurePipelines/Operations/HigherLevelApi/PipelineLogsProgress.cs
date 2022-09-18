using System.Collections.Generic;

namespace Components.AzurePipelines.Operations.HigherLevelApi;

public class PipelineLogsProgress
{
  public Dictionary<int, string> _returnValue;

  public PipelineLogsProgress(Dictionary<int, string> returnValue)
  {
    _returnValue = returnValue;
  }

  public bool AlreadyHasSomeLogsFor(Log logPageInfo)
  {
    return !_returnValue.ContainsKey(logPageInfo.Id);
  }

  public void Set(int id, string logContent)
  {
    _returnValue[id] = logContent;
  }

  public bool HasLessEntriesThanIn(string logContent, Log logPageInfo)
  {
    return logContent.Length > _returnValue[logPageInfo.Id].Length;
  }
}