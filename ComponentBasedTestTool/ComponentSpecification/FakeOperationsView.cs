using System.Linq;
using NUnit.Framework;
using ViewModels.ViewModels;

namespace ComponentSpecification
{
  public class FakeOperationsView
  {
    private readonly OperationsViewModel _operationsViewModel;

    public FakeOperationsView(OperationsViewModel operationsViewModel)
    {
      _operationsViewModel = operationsViewModel;
    }

    public void AssertShowsExactly(params string[] operationNames)
    {
      CollectionAssert.AreEqual(
        operationNames, 
        _operationsViewModel.Operations.Select(o => o.Name));
    }

    public void Select(string operationName)
    {
      _operationsViewModel.SelectedOperation = _operationsViewModel.Operations.First(o => o.Name == operationName);
    }
  }
}