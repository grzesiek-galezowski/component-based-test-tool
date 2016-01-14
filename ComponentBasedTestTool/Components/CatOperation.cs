using System;
using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints;

namespace Components
{
  public class CatOperation : Operation
  {
    private readonly OperationsOutput _out;
    private OperationParameter<string> _filename;

    public CatOperation(OperationsOutput @out)
    {
      _out = @out;
    }

    public async Task RunAsync(CancellationToken token)
    {
      _out.WriteLine("cat " + _filename.Value);
    }

    public void FillParameters(OperationParametersListBuilder parameters)
    {
      _filename = parameters.Path("Filename", "File.txt");
    }
  }
}