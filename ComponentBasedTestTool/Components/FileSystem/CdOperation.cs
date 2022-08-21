using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components.FileSystem;

public class CdOperation : IComponentOperation
{
  private readonly IOperationsOutput _out;
  private IOperationParameter<string> _path;

  public CdOperation(IOperationsOutput @out)
  {
    _out = @out;
  }

  public async Task RunAsync(CancellationToken token)
  {
    _out.WriteLine("cd " + _path.Value);

  }

  public void InitializeParameters(IOperationParametersListBuilder parameters)
  {
    _path = parameters.Path("Path", @"C:\");
  }

  public void StoreParameters(IPersistentStorage destination)
  {
    destination.Store(_path);
  }
}