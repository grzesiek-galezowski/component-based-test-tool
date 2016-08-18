using ComponentSpecification.AutomationLayer;
using Xbehave;

namespace ComponentSpecification
{
  public class GivenAnApplicationIsStartedWithNoComponentsAvailable
  {
    private readonly ComponentBasedTestToolDriver _context = new ComponentBasedTestToolDriver();

    [Background]
    public void Bg()
    {
      "Given the application is started"
        .x(() => _context.StartApplication());
    }
  

    [Scenario]
    public void ItShouldContainNoComponentsAvailableToSelect()
    {
      "Then the loaded components list should be blank"
        .x(() => _context.AssertNoComponentsAreLoaded());
    }

    [Scenario]
    public void ItShouldContainNoOperationsInTheOperationView()
    {
      "Then the loaded components list should be blank"
        .x(() => _context.AssertNoComponentsAreLoaded());
    }


    [Scenario]
    public void ItShouldContainNoComponentsInTheScriptView()
    {
      "Then the script view should be blank"
        .x(() => _context.ScriptView.AssertContainsNoOperations());
    }
  }
}
