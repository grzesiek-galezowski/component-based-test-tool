using System;
using ComponentSpecification.AutomationLayer;
using Xbehave;
using Xunit;
using static ComponentSpecification.DisplayingSpecification;

namespace ComponentSpecification
{
  public class GivenAnOperationIsStarted
  {
    private ComponentBasedTestToolDriver _context;

    [Background]
    public void Bg()
    {
      //GIVEN
      _context = new ComponentBasedTestToolDriver();
      var componentName1 = AnyComponentName();
      var operationName = AnyOperationName();

      _context.ComponentsSetup.Add(componentName1)
        .WithOperation(operationName);

      _context.StartApplication();
      _context.ComponentsView.AddInstanceOf(componentName1);
      _context.InstancesView.Select(componentName1);
      _context.OperationsView.Select(operationName);

      //WHEN
      _context.OperationsView.ExecuteSelectedOperation();
    }

    [Scenario]
    public void WhenItIsStoppedItsStatusShouldBeDisplayedAsStopped()
    {
      "When I stop it".x(() => _context.Operations.StopRunningOperation());

      "Then it should be displayed as stopped"
        .x(() => _context.OperationsView.AssertSelectedOperationIsDisplayedAsStopped());
    }
  }
}
