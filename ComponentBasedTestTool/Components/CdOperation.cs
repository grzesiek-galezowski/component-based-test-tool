using System;
using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components;

public class CdOperation : ComponentOperation
{
  private readonly OperationsOutput _out;
  private OperationParameter<string> _path;

  public CdOperation(OperationsOutput @out)
  {
    _out = @out;
  }

  public async Task RunAsync(CancellationToken token)
  {
    _out.WriteLine("cd " + _path.Value);

  }

  public void InitializeParameters(OperationParametersListBuilder parameters)
  {
    _path = parameters.Path("Path", @"C:\");
  }

  public void StoreParameters(PersistentStorage destination)
  {
    destination.Store(_path);
  }
}