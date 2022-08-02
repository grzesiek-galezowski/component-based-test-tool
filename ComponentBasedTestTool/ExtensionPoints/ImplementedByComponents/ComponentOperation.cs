using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints.ImplementedByContext;

namespace ExtensionPoints.ImplementedByComponents;

public interface IRunnable
{
  Task RunAsync(CancellationToken token);
}

public interface IComponentOperation : IRunnable
{
  void InitializeParameters(IOperationParametersListBuilder parameters);
  void StoreParameters(IPersistentStorage destination);
}