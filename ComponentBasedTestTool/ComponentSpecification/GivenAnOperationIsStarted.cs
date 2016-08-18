using ComponentSpecification.AutomationLayer;
using TddEbook.TddToolkit;
using Xbehave;
using static ComponentSpecification.ComponentAny;

namespace ComponentSpecification
{
  public class GivenAnOperationIsStarted
  {
    private readonly ComponentBasedTestToolDriver _context = new ComponentBasedTestToolDriver();
    private readonly string _componentName1 = AnyComponentName();
    private readonly string _operationName11 = AnyOperationName();

    [Background]
    public void Bg()
    {
      //GIVEN
      "Given an operation is started"
        .x(() =>
        {
          _context.ComponentsSetup.Add(_componentName1)
            .WithOperation(_operationName11);

          _context.StartApplication();
          _context.ComponentsView.AddInstanceOf(_componentName1);
          _context.StartOperation(_componentName1, _operationName11);
        });
    }

    [Scenario]
    public void ShouldStartPluginOperation()
    {
      "Then the plugin operation should be started"
        .x(() => _context.Operations.AssertWasRun(_operationName11));
    }


    [Scenario]
    public void ShouldDisplayStartedOperationAsInProgress()
    {
      "Then it should be displayed as in progress"
        .x(() => _context.OperationsView.AssertSelectedOperationIsDisplayedAsInProgress());
    }

    [Scenario]
    public void ShouldDisplayStoppedOperationAsStopped()
    {
      "When I stop it".x(() => _context.Operations.MakeRunningOperationStop());

      "Then it should be displayed as stopped"
        .x(() => _context.OperationsView.AssertSelectedOperationIsDisplayedAsStopped());
    }

    [Scenario]
    public void ShouldDisplaySuccessfulOperationAsSuccessful()
    {
      "When it finishes successfully"
        .x(() => _context.Operations.MakeRunningOperationSucceed());

      "Then the operations should be displayed as successful"
        .x(() => _context.OperationsView.AssertSelectedOperationIsDisplayedAsSuccessful());
    }

    [Scenario]
    public void ShouldDisplayFailedOperationAsFailed()
    {
      var exception = Any.Exception();

      "When it fails"
        .x(() => _context.Operations.MakeRunningOperationFailWith(exception));

      "Then the operations should be displayed as failed"
        .x(() => _context.OperationsView.AssertSelectedOperationIsDisplayedAsFailedWith(exception));
    }

  }
}
