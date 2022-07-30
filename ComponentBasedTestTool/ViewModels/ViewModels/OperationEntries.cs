using System.Collections.Generic;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.ViewModels.Ports;
using Core.Maybe;
using ViewModels.Composition;

namespace ViewModels.ViewModels;

public class OperationEntries
{
  private readonly BackgroundTasks _backgroundTasks;
  private readonly List<OperationEntry> _operations;

  public OperationEntries(BackgroundTasks backgroundTasks)
  {
    _backgroundTasks = backgroundTasks;
    _operations = new List<OperationEntry>();
  }

  public void Add(string componentInstanceName, string name, OperationStateMachine operation, Maybe<string> dependencyName)
  {
    _operations.Add(OperationEntry.With(componentInstanceName, name, operation, dependencyName, operation));
  }

  public OperationViewModelsSource ConvertUsing(OperationViewModelFactory operationViewModelFactory)
  {
    var operationViewModels = OperationViewModelsSource
      .CreateOperationViewModels(operationViewModelFactory, _operations);

    return operationViewModels;
  }
}