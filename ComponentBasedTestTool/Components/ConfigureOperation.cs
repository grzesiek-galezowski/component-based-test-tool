using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components
{
  public class ConfigureOperation : ComponentOperation
  {
    private readonly OperationsOutput _output;

    public ConfigureOperation(OperationsOutput output)
    {
      _output = output;
    }

    public Task RunAsync(CancellationToken token)
    {
      return Task.CompletedTask;
    }

    public void InitializeParameters(OperationParametersListBuilder parameters)
    {
      
    }

    public void StoreParameters(PersistentStorage destination)
    {

    }
  }
}