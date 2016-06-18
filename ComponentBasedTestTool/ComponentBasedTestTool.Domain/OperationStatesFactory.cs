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

    public OperationState NotExecutable()
    {
      return new NotExecutableOperationState();
    }

    public OperationState InProgress()
    {
      return new InProgressOperationState();
    }

    public OperationState ExecutableState()
    {
      return new ExecutableOperationState(_cancellationTokenSource);
    }
  }
}