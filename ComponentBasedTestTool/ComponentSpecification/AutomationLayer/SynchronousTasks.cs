using System;
using System.Threading;
using System.Threading.Tasks;
using ComponentBasedTestTool.Domain.OperationStates;

namespace ComponentSpecification.AutomationLayer
{
  public class SynchronousTasks : BackgroundTasks
  {
    public void Launch<T2, T3>(Func<CancellationTokenSource, T2, T3, Task> func, T2 arg2, T3 arg3)
    {
      using (var cancellationTokenSource = new CancellationTokenSource())
      {
        func(cancellationTokenSource, arg2, arg3);
      }
    }
  }
}