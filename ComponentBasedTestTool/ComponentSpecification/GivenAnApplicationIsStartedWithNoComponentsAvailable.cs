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
      _context.StartApplication();
    }
  

    [Scenario]
    public void ItShouldContainNoComponentsAvailableToSelect()
    {
      "Then the loaded components list should be blank"
        .x(() => _context.AssertNoComponentsAreLoaded());
    }
  }
}
