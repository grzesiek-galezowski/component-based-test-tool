using System;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components.FileSystem;

public class WaitOperation : IComponentOperation
{
  private readonly IOperationsOutput _out;
  private IOperationParameter<TimeSpan> _time;

  public WaitOperation(IOperationsOutput @out)
  {
    _out = @out;
  }

  public async Task RunAsync(CancellationToken token)
  {
    var seconds = _time.Value.Seconds;
    while (seconds > 0)
    {
      token.ThrowIfCancellationRequested();
      _out.WriteLine(seconds.ToString());
      await Task.Delay(1000, token).ConfigureAwait(false);
      seconds--;
    }
    _out.WriteLine(seconds.ToString());
  }

  public void InitializeParameters(IOperationParametersListBuilder parameters)
  {
    _time = parameters.Seconds("Time (s)", 5);

  }

  public void StoreParameters(IPersistentStorage destination)
  {
    destination.Store(_time);
  }
}