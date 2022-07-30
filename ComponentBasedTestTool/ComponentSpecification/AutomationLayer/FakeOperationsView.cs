using System;
using System.Linq;
using ViewModels.ViewModels;
using Xunit;

namespace ComponentSpecification.AutomationLayer;

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
    Assert.Equal("Stopped", _operationsViewModel.SelectedOperation.State);
  }

  public void AssertSelectedOperationIsDisplayedAsSuccessful()
  {
    Assert.Equal("Success", _operationsViewModel.SelectedOperation.State);
  }

  public void AssertSelectedOperationIsDisplayedAsInProgress()
  {
    Assert.Equal("In Progress", _operationsViewModel.SelectedOperation.State);
  }

  public void AssertSelectedOperationIsDisplayedAsFailedWith(Exception exception)
  {
    Assert.Equal("Failure", _operationsViewModel.SelectedOperation.State);
    Assert.Equal(exception.ToString(), _operationsViewModel.SelectedOperation.LastErrorFullText);
  }

  public void AddSelectedOperationToScriptView()
  {
    _operationsViewModel.SelectedOperation.AddToScriptViewCommand.Execute(null);
  }
}