using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components.FileSystem;

public class LsOperation : IComponentOperation
{
  private readonly IOperationsOutput _out;
  private IOperationParameter<string> _directory;
  private IOperationParameter<bool> _displayAll;
  private IOperationParameter<bool> _recursive;

  public LsOperation(IOperationsOutput @out)
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

  public void InitializeParameters(IOperationParametersListBuilder parameters)
  {
    _directory = parameters.Path("Directory", @"C:\");
    _displayAll = parameters.Flag("Display all", true);
    _recursive = parameters.Flag("Recursive", true);
  }

  public void StoreParameters(IPersistentStorage destination)
  {
    destination.Store(
      _directory,
      _displayAll,
      _recursive);
  }
}