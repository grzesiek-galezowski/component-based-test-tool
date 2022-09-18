using Components.AzurePipelines.Client.Dto;
using ExtensionPoints.ImplementedByContext;

namespace Components.AzurePipelines.Operations;

public interface IPipelineObserver
{
  void NotifyAboutNew(RunDto runInfo);
  void NotifyAboutLogs(RunDto runInfo, Log logPageInfo, string logContent);
}

public class PipelineObserver : IPipelineObserver
{
  private readonly IOperationsOutput _operationsOutput;

  public PipelineObserver(IOperationsOutput operationsOutput)
  {
    _operationsOutput = operationsOutput;
  }

  public void NotifyAboutNew(RunDto runInfo)
  {
    _operationsOutput.WriteLine(runInfo.ToString());
  }

  public void NotifyAboutLogs(RunDto runInfo, Log logPageInfo, string logContent)
  {
    _operationsOutput.WriteLine(logContent);
  }
}