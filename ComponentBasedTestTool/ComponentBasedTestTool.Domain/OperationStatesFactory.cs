using System.Threading;
using ComponentBasedTestTool.Domain.OperationStates;

namespace ComponentBasedTestTool.Domain
{
  public class OperationStatesFactory
  {
    private readonly BackgroundTasks _backgroundTasks;

    public OperationStatesFactory(BackgroundTasks backgroundTasks)
    {
      _backgroundTasks = backgroundTasks;
    }

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
      return new RunnableOperationState(_backgroundTasks);
    }
  }
}