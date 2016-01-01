using System;
using System.Threading.Tasks;
using ComponentBasedTestTool.ViewModels.OperationStates;
using ExtensionPoints;

namespace Components
{
  public class CdOperation : Operation
  {
    private readonly OperationsOutput _out;

    public CdOperation(OperationsOutput @out)
    {
      _out = @out;
    }

    public async Task RunAsync()
    {
      _out.Write("cd lolek" + Environment.NewLine);
    }
  }
}