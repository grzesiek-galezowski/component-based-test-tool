using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints.ImplementedByContext;

namespace ExtensionPoints.ImplementedByComponents;

public interface Runnable
{
  Task RunAsync(CancellationToken token);
}

public interface ComponentOperation : Runnable
{
  void InitializeParameters(OperationParametersListBuilder parameters);
  void StoreParameters(PersistentStorage destination);
}