using ComponentSpecification.AutomationLayer;
using Xunit;

namespace ComponentSpecification;

public class GivenAnApplicationIsStartedWithNoComponentsAvailable
{
  [Fact]
  public void ItShouldContainNoComponentsAvailableToSelect()
  {
    var context = new ComponentBasedTestToolDriver();
    context.StartApplication();
    context.AssertNoComponentsAreLoaded();
  }

  [Fact]
  public void ItShouldContainNoOperationsInTheOperationView()
  {
    var context = new ComponentBasedTestToolDriver();
    context.StartApplication();

    context.AssertNoComponentsAreLoaded();
  }

  [Fact]
  public void ItShouldContainNoComponentsInTheScriptView()
  {
    var context = new ComponentBasedTestToolDriver();
    context.StartApplication();
    context.ScriptView.AssertContainsNoOperations();
  }
}