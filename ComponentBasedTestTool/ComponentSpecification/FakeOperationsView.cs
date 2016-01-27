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
        _operationsViewModel.Operations.Select(o => o.Name).ToArray());
    }

    public void Select(string operationName)
    {
      _operationsViewModel.SelectedOperation = OperationViewByName(operationName);
    }

    private OperationViewModel OperationViewByName(string operationName)
    {
      return _operationsViewModel.Operations.First(o => o.Name == operationName);
    }

    public void Execute(string operationName)
    {
      OperationViewByName(operationName).RunOperationCommand.Execute(null);
    }

    public void ExecuteSelectedOperation()
    {
      _operationsViewModel.SelectedOperation.RunOperationCommand.Execute(null);
    }
  }
}