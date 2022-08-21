using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components.FileSystem;

public class CatOperation : IComponentOperation
{
  private readonly IOperationsOutput _out;
  private IOperationParameter<string> _filename;

  public CatOperation(IOperationsOutput @out)
  {
    _out = @out;
  }

  public async Task RunAsync(CancellationToken token)
  {
    _out.WriteLine("cat " + _filename.Value);
  }

  public void InitializeParameters(IOperationParametersListBuilder parameters)
  {
    _filename = parameters.Path("Filename", "File.txt");
  }

  public void StoreParameters(
    IPersistentStorage destination)
  {
    destination.Store(_filename);
  }
}