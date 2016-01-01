using System;
using System.Threading.Tasks;
using ComponentBasedTestTool.ViewModels.OperationStates;
using ExtensionPoints;

namespace Components
{
  public class CatOperation : Operation
  {
    private readonly OperationsOutput _out;

    public CatOperation(OperationsOutput @out)
    {
      _out = @out;
    }

    public async Task RunAsync()
    {
      _out.Write("cat Folder" + Environment.NewLine);
    }
  }
}