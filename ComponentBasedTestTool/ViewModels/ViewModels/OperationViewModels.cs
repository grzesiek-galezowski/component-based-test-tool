using System;
using System.Collections.Generic;
using System.Linq;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;

namespace ViewModels.ViewModels
{
  public class OperationViewModels
  {
    private readonly List<OperationViewModel> _viewModels;

    public OperationViewModels(List<OperationViewModel> viewModels)
    {
      this._viewModels = viewModels;
    }

    public static OperationViewModels CreateOperationViewModels(
      OperationViewModelFactory operationViewModelFactory, 
      List<OperationEntry> operationEntries)
    {
      var operationViewModels = new OperationViewModels(
        operationEntries.Select(
          NewOperationViewModel(operationViewModelFactory)).ToList());
      operationViewModels.ResolveDependencies();

      return operationViewModels;
    }

    private static Func<OperationEntry, OperationViewModel> NewOperationViewModel(OperationViewModelFactory operationViewModelFactory)
    {
      return operationEntry =>
      {
        var operationStateMachine = operationEntry.OperationStateMachine;
        var operationViewModel = operationViewModelFactory.CreateOperationViewModel(operationEntry, operationStateMachine);
        operationStateMachine.SetContext(operationViewModel);
        return operationViewModel;
      };
    }

    private void ResolveDependencies()
    {
      foreach (var operationViewModel in _viewModels)
      {
        operationViewModel.ObserveMatching(_viewModels);
      }
    }

    public void AddTo(OperationsViewModel operationsViewModel)
    {
      operationsViewModel.AddOperations(_viewModels);
    }
  }
}