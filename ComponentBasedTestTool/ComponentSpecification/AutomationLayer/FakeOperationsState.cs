using Core.Maybe;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentSpecification.AutomationLayer;

public class FakeOperationsState
{
  private readonly string _instanceName;
  private readonly string _operationName;
  private readonly FakeTestComponents _componentsSetup;
  private readonly Maybe<OperationContext> _runningOperationContext;

  public FakeOperationsState(string instanceName, string operationName, FakeTestComponents componentsSetup, Maybe<OperationContext> runningOperationContext)
  {
    _instanceName = instanceName;
    _operationName = operationName;
    _componentsSetup = componentsSetup;
    _runningOperationContext = runningOperationContext;
  }

  public void AssertWasRun(string operationName, int times = 1)
  {
    _componentsSetup
      .RetrieveOperation(_instanceName, operationName)
      .AssertWasRun(times);
  }

  public void MakeRunningOperationStop()
  {
    if (_runningOperationContext.HasValue)
    {
      _runningOperationContext.Value().Stopped();
    }
    else
    {
      throw new NoOperationRunningException();
    }

  }

  public void MakeRunningOperationSucceed()
  {
    if (_runningOperationContext.HasValue)
    {
      _runningOperationContext.Value().Success();
    }
    else
    {
      throw new NoOperationRunningException();
    }
  }

  public void MakeRunningOperationFailWith(Exception exception)
  {
    if (_runningOperationContext.HasValue)
    {
      _runningOperationContext.Value().Failure(exception);
    }
    else
    {
      throw new NoOperationRunningException();
    }
  }
}