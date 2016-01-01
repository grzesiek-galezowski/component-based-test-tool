using System;
using System.Threading.Tasks;
using ComponentBasedTestTool.ViewModels.OperationStates;
using ExtensionPoints;

namespace Components
{
  public class LsOperation : Operation
  {
    private readonly OperationsOutput _out;

    public LsOperation(OperationsOutput @out)
    {
      _out = @out;
    }

    public async Task RunAsync()
    {
      _out.Write("lolki2" + Environment.NewLine);
      await Task.Delay(2000);
      _out.Write("123123123" + Environment.NewLine);
    }
  }
}