using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints.ImplementedByContext;

namespace ExtensionPoints.ImplementedByComponents
{
  public interface Operation
  {
    Task RunAsync(CancellationToken token);
    void InitializeParameters(OperationParametersListBuilder parameters);
    void StoreParameters(PersistentStorage destination);
  }
}