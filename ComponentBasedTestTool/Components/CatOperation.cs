using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components;

public class CatOperation : ComponentOperation
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

  public void InitializeParameters(OperationParametersListBuilder parameters)
  {
    _filename = parameters.Path("Filename", "File.txt");
  }

  public void StoreParameters(
    PersistentStorage destination)
  {
    destination.Store(_filename);
  }
}