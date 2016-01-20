using System;
using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components
{
  public class LsOperation : Operation
  {
    private readonly OperationsOutput _out;
    private OperationParameter<string> _directory;
    private OperationParameter<bool> _displayAll;
    private OperationParameter<bool> _recursive;

    public LsOperation(OperationsOutput @out)
    {
      _out = @out;
    }

    public async Task RunAsync(CancellationToken token)
    {
      _out.WriteLine("ls" + 
        (_displayAll.Value ? " -l" : "") + 
        (_recursive.Value ? " -R" : "") +
         " " + _directory.Value);
    }

    public void FillParameters(OperationParametersListBuilder parameters)
    {
      _directory = parameters.Path("Directory", @"C:\");
      _displayAll = parameters.Flag("Display all", true);
      _recursive = parameters.Flag("Recursive", true);
    }
  }
}