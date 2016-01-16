using System.Collections.Generic;
using System.Linq;

namespace ViewModels.ViewModels
{
  public class OperationViewModels
  {
    private readonly List<OperationViewModel> _viewModels;

    public OperationViewModels(List<OperationViewModel> viewModels)
    {
      this._viewModels = viewModels;
    }

    public static OperationViewModels CreateOperationViewModels(OperationViewModelFactory operationViewModelFactory, List<OperationEntry> operationEntries)
    {
      var operationViewModels = new OperationViewModels(operationEntries.Select(
        operationViewModelFactory.CreateOperationViewModel).ToList());
      operationViewModels.ResolveDependencies();

      return operationViewModels;
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