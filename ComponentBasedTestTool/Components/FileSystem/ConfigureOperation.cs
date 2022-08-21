using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components.FileSystem;

public class ConfigureOperation : IComponentOperation
{
  private readonly IOperationsOutput _output;

  public ConfigureOperation(IOperationsOutput output)
  {
    _output = output;
  }

  public Task RunAsync(CancellationToken token)
  {
    return Task.CompletedTask;
  }

  public void InitializeParameters(IOperationParametersListBuilder parameters)
  {

  }

  public void StoreParameters(IPersistentStorage destination)
  {

  }
}