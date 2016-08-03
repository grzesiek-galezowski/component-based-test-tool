using System;
using System.Linq;
using TddEbook.TddToolkit;
using ViewModels.ViewModels;
using Xunit;

namespace ComponentSpecification.AutomationLayer
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
      Assert.Equal(
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

    public void StartSelectedOperation()
    {
      _operationsViewModel.SelectedOperation.RunOperationCommand.Execute(null);
    }

    public void AssertSelectedOperationIsDisplayedAsStopped()
    {
      XAssert.Equal("Stopped", _operationsViewModel.SelectedOperation.State);
    }

    public void AssertSelectedOperationIsDisplayedAsSuccessful()
    {
      XAssert.Equal("Success", _operationsViewModel.SelectedOperation.State);
    }

    public void AssertSelectedOperationIsDisplayedAsInProgress()
    {
      XAssert.Equal("In Progress", _operationsViewModel.SelectedOperation.State);
    }

    public void AssertSelectedOperationIsDisplayedAsFailedWith(Exception exception)
    {
      XAssert.Equal("Failure", _operationsViewModel.SelectedOperation.State);
      XAssert.Equal(exception.ToString(), _operationsViewModel.SelectedOperation.LastErrorFullText);
    }
  }
}