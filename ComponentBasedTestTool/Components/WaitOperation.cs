using System;
using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints;

namespace Components
{
  public class WaitOperation : Operation
  {
    private readonly OperationsOutput _out;
    private OperationParameter<TimeSpan> _time;

    public WaitOperation(OperationsOutput @out)
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
        await Task.Delay(1000);
        seconds--;
      }
      _out.WriteLine(seconds.ToString());
    }

    public void FillParameters(OperationParametersListBuilder parameters)
    {
      _time = parameters.Seconds("Time (s)", 5);
      
    }
  }
}