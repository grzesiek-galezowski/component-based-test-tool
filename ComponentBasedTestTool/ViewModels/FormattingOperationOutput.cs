using ExtensionPoints;

namespace ViewModels
{
  public class FormattingOperationOutput : OperationsOutput
  {
    private readonly string _operationName;
    private readonly OperationsOutput _output;

    public FormattingOperationOutput(string operationName, OperationsOutput output)
    {
      _operationName = operationName;
      _output = output;
    }

    public void WriteLine(string text)
    {
      _output.WriteLine($"[{_operationName}]" + ": " + text);
    }
  }
}