using System.Threading;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;

namespace ComponentBasedTestTool.Domain;

public class OperationStatesFactory
{
  private readonly IBackgroundTasks _backgroundTasks;

  public OperationStatesFactory(IBackgroundTasks backgroundTasks)
  {
    _backgroundTasks = backgroundTasks;
  }

  public IOperationState Unavailable()
  {
    return new UnavailableOperationState();
  }

  public IOperationState InProgress(CancellationTokenSource cancellationTokenSource)
  {
    return new InProgressOperationState(cancellationTokenSource);
  }

  public IOperationState RunnableState()
  {
    return new RunnableOperationState(_backgroundTasks);
  }
}