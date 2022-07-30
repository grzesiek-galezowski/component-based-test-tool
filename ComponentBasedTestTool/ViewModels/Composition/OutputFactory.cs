using ExtensionPoints.ImplementedByContext;
using ViewModels.ViewModels;

namespace ViewModels.Composition;

public class OutputFactory
{
  private readonly OperationsOutputViewModel _operationsOutputViewModel;

  public OutputFactory(OperationsOutputViewModel operationsOutputViewModel)
  {
    _operationsOutputViewModel = operationsOutputViewModel;
  }

  public OperationsOutput CreateOutFor(string operationName)
  {
    return new FormattingOperationOutput(operationName, _operationsOutputViewModel);
  }
}