using System.Collections.Generic;
using System.Linq;
using CallMeMaybe;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
using ViewModels.Composition;

namespace ViewModels.ViewModels
{
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
}