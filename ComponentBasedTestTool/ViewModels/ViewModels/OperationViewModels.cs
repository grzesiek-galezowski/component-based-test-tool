using System.Collections.Generic;
using System.Linq;
using ViewModels.Composition;

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
      IEnumerable<OperationEntry> operationEntries)
    {
      var operationViewModels = new OperationViewModels(
        operationEntries.Select(
          operationEntry => operationEntry.ToOperationViewModel(operationViewModelFactory)).ToList());
      operationViewModels.Register();
      operationViewModels.ResolveDependencies();

      return operationViewModels;
    }

    private void Register()
    {
      foreach (var operationViewModel in _viewModels)
      {
        operationViewModel.ObserveOperationState();
      }
    }

    private void ResolveDependencies()
    {
      foreach (var operationViewModel in _viewModels)
      {
        operationViewModel.ObserveDependencies(_viewModels);
      }
    }

    public void AddTo(OperationsViewModel operationsViewModel)
    {
      operationsViewModel.AddOperations(_viewModels);
    }
  }
}