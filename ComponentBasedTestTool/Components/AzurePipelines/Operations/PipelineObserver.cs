using Components.AzurePipelines.Client.Dto;
using ExtensionPoints.ImplementedByContext;

namespace Components.AzurePipelines.Operations;

public interface IPipelineObserver
{
  void NotifyAboutNew(Run runInfo);
  void NotifyAboutLogs(Run runInfo, Log logPageInfo, string logContent);
}

public class PipelineObserver : IPipelineObserver
{
  private readonly IOperationsOutput _operationsOutput;

  public PipelineObserver(IOperationsOutput operationsOutput)
  {
    _operationsOutput = operationsOutput;
  }

  public void NotifyAboutNew(Run runInfo)
  {
    _operationsOutput.WriteLine(runInfo.ToString());
  }

  public void NotifyAboutLogs(Run runInfo, Log logPageInfo, string logContent)
  {
    _operationsOutput.WriteLine(logContent);
  }
}