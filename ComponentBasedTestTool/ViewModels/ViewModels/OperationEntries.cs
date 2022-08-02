using System.Collections.Generic;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.ViewModels.Ports;
using Core.Maybe;
using ViewModels.Composition;

namespace ViewModels.ViewModels;

public class OperationEntries
{
  private readonly IBackgroundTasks _backgroundTasks;
  private readonly List<OperationEntry> _operations;

  public OperationEntries(IBackgroundTasks backgroundTasks)
  {
    _backgroundTasks = backgroundTasks;
    _operations = new List<OperationEntry>();
  }

  public void Add(string componentInstanceName, string name, IOperationStateMachine operation, Maybe<string> dependencyName)
  {
    _operations.Add(OperationEntry.With(componentInstanceName, name, operation, dependencyName, operation));
  }

  public OperationViewModelsSource ConvertUsing(IOperationViewModelFactory operationViewModelFactory)
  {
    var operationViewModels = OperationViewModelsSource
      .CreateOperationViewModels(operationViewModelFactory, _operations);

    return operationViewModels;
  }
}