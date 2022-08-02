using ExtensionPoints.ImplementedByContext;

namespace ViewModels;

public class FormattingOperationOutput : IOperationsOutput
{
  private readonly string _operationName;
  private readonly IOperationsOutput _output;

  public FormattingOperationOutput(string operationName, IOperationsOutput output)
  {
    _operationName = operationName;
    _output = output;
  }

  public void WriteLine(string text)
  {
    _output.WriteLine($"[{_operationName}]" + ": " + text);
  }
}