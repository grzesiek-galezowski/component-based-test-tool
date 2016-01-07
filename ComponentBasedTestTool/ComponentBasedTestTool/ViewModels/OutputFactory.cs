using ExtensionPoints;

namespace ComponentBasedTestTool.ViewModels
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