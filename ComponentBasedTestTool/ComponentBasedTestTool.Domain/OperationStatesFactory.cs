using System.Threading;
using ComponentBasedTestTool.Domain.OperationStates;

namespace ComponentBasedTestTool.Domain
{
  public class OperationStatesFactory
  {
    public OperationState Unavailable()
    {
      return new UnavailableOperationState();
    }

    public OperationState InProgress(CancellationTokenSource cancellationTokenSource)
    {
      return new InProgressOperationState(cancellationTokenSource);
    }

    public OperationState RunnableState()
    {
      return new RunnableOperationState();
    }
  }
}