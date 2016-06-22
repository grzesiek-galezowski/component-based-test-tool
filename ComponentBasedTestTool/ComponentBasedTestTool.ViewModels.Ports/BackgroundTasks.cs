using System;
using System.Threading;
using System.Threading.Tasks;

namespace ComponentBasedTestTool.Domain.OperationStates
{
  public interface BackgroundTasks
  {
    void Launch<T2, T3>(Func<CancellationTokenSource, T2, T3, Task> func, T2 arg2, T3 arg3);
  }
}