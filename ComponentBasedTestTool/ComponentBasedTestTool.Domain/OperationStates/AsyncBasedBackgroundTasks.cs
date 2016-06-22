using System;
using System.Threading;
using System.Threading.Tasks;

namespace ComponentBasedTestTool.Domain.OperationStates
{
  public class AsyncBasedBackgroundTasks : BackgroundTasks
  {
    public void Launch<T2, T3>(Func<CancellationTokenSource, T2, T3, Task> func, T2 arg2, T3 arg3)
    {
      var cancellationTokenSource = new CancellationTokenSource();
      Task.Run(async () =>
      {
        await func(cancellationTokenSource, arg2, arg3).ConfigureAwait(false);
      })
      .ContinueWith((t,o) => cancellationTokenSource.Dispose(), null);
    }
  }
}