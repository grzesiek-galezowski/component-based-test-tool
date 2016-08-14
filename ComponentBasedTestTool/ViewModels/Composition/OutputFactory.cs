using ExtensionPoints;
using ExtensionPoints.ImplementedByContext;

namespace ViewModels.ViewModels
{
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
}