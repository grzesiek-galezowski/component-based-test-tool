using System;
using Core.Maybe;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components.AzurePipelines;

public class AzurePipelinesComponent : ICoreTestComponent
{
  private const string ConfigureOpName = "Configure";
  private const string ListPipelinesOpName = "List Pipelines";
  private const string RunPipelineOpName = "Run Pipeline";
  private const string MonitorPipelineOpName = "Monitor Pipeline";
  private const string GetPipelineLogsOpName = "Get pipeline logs";
  private readonly AzurePipelinesComponentConfiguration _config = new();
  private Maybe<IOperationControl> _configureOperation;
  private Maybe<IOperationControl> _listPipelinesOperation;
  private Maybe<IOperationControl> _runPipelineOperation;
  private Maybe<IOperationControl> _monitorPipelineOperation;
  private Maybe<IOperationControl> _getLogsPipelineOperation;

  public void PopulateOperations(ITestComponentOperationDestination ctx)
  {
    ctx.AddOperation(ConfigureOpName, _configureOperation.Value());
    ctx.AddOperation(ListPipelinesOpName, _listPipelinesOperation.Value(), ConfigureOpName);
    ctx.AddOperation(RunPipelineOpName, _runPipelineOperation.Value(), ConfigureOpName);
    ctx.AddOperation(MonitorPipelineOpName, _monitorPipelineOperation.Value(), ConfigureOpName);
    ctx.AddOperation(GetPipelineLogsOpName, _getLogsPipelineOperation.Value(), ConfigureOpName);
  }

  public void CreateOperations(ITestComponentContext ctx)
  {
    _configureOperation =
      ctx.CreateOperation(new ConfigureAzurePipelinesComponentInstanceOperation(_config)).Just();
    _listPipelinesOperation =
      ctx.CreateOperation(new ListPipelinesOperation(
        ctx.CreateOutFor(ListPipelinesOpName),
        _config)).Just();
    _runPipelineOperation =
      ctx.CreateOperation(new RunPipelineOperation(
        ctx.CreateOutFor(RunPipelineOpName),
        _config)).Just();
    _monitorPipelineOperation =
      ctx.CreateOperation(new MonitorPipelineOperation(
        ctx.CreateOutFor(MonitorPipelineOpName),
        _config)).Just();
    _getLogsPipelineOperation =
      ctx.CreateOperation(new GetPipelineLogsOperation(
        ctx.CreateOutFor(GetPipelineLogsOpName),
        _config)).Just();
  }

}
