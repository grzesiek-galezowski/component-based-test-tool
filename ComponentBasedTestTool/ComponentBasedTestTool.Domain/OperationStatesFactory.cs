using System.Threading;
using ComponentBasedTestTool.Domain.OperationStates;

namespace ComponentBasedTestTool.Domain
{
  public class OperationStatesFactory
  {
    private readonly CancellationTokenSource _cancellationTokenSource;

    public OperationStatesFactory(CancellationTokenSource cancellationTokenSource)
    {
      _cancellationTokenSource = cancellationTokenSource;
    }

    public OperationState Unavailable()
    {
      return new UnavailableOperationState();
    }

    public OperationState InProgress()
    {
      return new InProgressOperationState();
    }

    public OperationState RunnableState()
    {
      return new RunnableOperationState(_cancellationTokenSource);
    }
  }
}