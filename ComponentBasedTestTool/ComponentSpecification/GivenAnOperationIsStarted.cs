using ComponentSpecification.AutomationLayer;
using TddXt.AnyRoot;
using Xunit;

namespace ComponentSpecification;

public class GivenAnOperationIsStarted
{

  [Fact]
  public void ShouldStartPluginOperation()
  {
    var context = new ComponentBasedTestToolDriver();
    var componentName1 = Any.ComponentName();
    var operationName11 = Any.OperationName();

    //GIVEN
    context.ComponentsSetup.Add(componentName1)
      .WithOperation(operationName11);

    context.StartApplication();
    context.ComponentsView.AddInstanceOf(componentName1);
    context.StartOperation(componentName1, operationName11);

    //THEN
    context.Operations.AssertWasRun(operationName11);
  }


  [Fact]
  public void ShouldDisplayStartedOperationAsInProgress()
  {
    //GIVEN
    var context = new ComponentBasedTestToolDriver();
    var componentName1 = Any.ComponentName();
    var operationName11 = Any.OperationName();

    context.ComponentsSetup.Add(componentName1)
      .WithOperation(operationName11);

    context.StartApplication();
    context.ComponentsView.AddInstanceOf(componentName1);

    //WHEN
    context.StartOperation(componentName1, operationName11);

    //THEN
    context.OperationsView.AssertSelectedOperationIsDisplayedAsInProgress();
  }

  [Fact]
  public void ShouldDisplayStoppedOperationAsStopped()
  {
    //GIVEN
    var context = new ComponentBasedTestToolDriver();
    var componentName1 = Any.ComponentName();
    var operationName11 = Any.OperationName();

    context.ComponentsSetup.Add(componentName1)
      .WithOperation(operationName11);

    context.StartApplication();
    context.ComponentsView.AddInstanceOf(componentName1);
    context.StartOperation(componentName1, operationName11);

    //WHEN
    context.Operations.MakeRunningOperationStop();

    context.OperationsView.AssertSelectedOperationIsDisplayedAsStopped();
  }

  [Fact]
  public void ShouldDisplaySuccessfulOperationAsSuccessful()
  {
    //GIVEN
    var context = new ComponentBasedTestToolDriver();
    var componentName1 = Any.ComponentName();
    var operationName11 = Any.OperationName();

    context.ComponentsSetup.Add(componentName1)
      .WithOperation(operationName11);

    context.StartApplication();
    context.ComponentsView.AddInstanceOf(componentName1);
    context.StartOperation(componentName1, operationName11);

    context.Operations.MakeRunningOperationSucceed();

    context.OperationsView.AssertSelectedOperationIsDisplayedAsSuccessful();
  }

  [Fact]
  public void ShouldDisplayFailedOperationAsFailed()
  {
    //GIVEN
    var context = new ComponentBasedTestToolDriver();
    var componentName1 = Any.ComponentName();
    var operationName11 = Any.OperationName();
    var exception = Any.Exception();

    context.ComponentsSetup.Add(componentName1)
      .WithOperation(operationName11);

    context.StartApplication();
    context.ComponentsView.AddInstanceOf(componentName1);
    context.StartOperation(componentName1, operationName11);

    //WHEN
    context.Operations.MakeRunningOperationFailWith(exception);

    //THEN
    context.OperationsView.AssertSelectedOperationIsDisplayedAsFailedWith(exception);
  }

}