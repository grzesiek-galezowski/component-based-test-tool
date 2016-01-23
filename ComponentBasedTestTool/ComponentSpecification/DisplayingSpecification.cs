using NUnit.Framework;
using TddEbook.TddToolkit;

namespace ComponentSpecification
{
  public class DisplayingSpecification
  {
    [Test]
    public void ShouldShowNoComponentWhenNoComponentWasLoaded()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      //WHEN
      context.StartApplication();

      //THEN
      context.AssertNoComponentsAreLoaded();
    }

    [Test]
    public void ShouldShowTheNamesOfTheAddedComponents()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var pluginName1 = Any.String();
      var pluginName2 = Any.String();
      var pluginName3 = Any.String();
      context.AddComponentsWithNames(pluginName1, pluginName2, pluginName3);

      //WHEN
      context.StartApplication();

      //THEN
      context.ComponentsView
        .AssertExactlyTheFollowingComponentsAreLoaded2(new[] {pluginName1, pluginName2, pluginName3});
    }

    [Test] //note one step further- maybe nested contexts?
    public void ShouldShowNameOfTheAddedInstances()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var pluginName1 = Any.String();
      var pluginName2 = Any.String();
      var pluginName3 = Any.String();
      context.AddComponentsWithNames(pluginName1, pluginName2, pluginName3);

      context.StartApplication();

      //WHEN
      context.AddInstanceOf(pluginName1);
      context.AddInstanceOf(pluginName1);
      context.AddInstanceOf(pluginName2);

      //THEN
      context.InstancesView.AssertShowsExactlyTheFollowingInstances(pluginName1, 2);
      context.InstancesView.AssertShowsExactlyTheFollowingInstances(pluginName2, 1);
      context.InstancesView.AssertShowsExactlyTheFollowingInstances(pluginName3, 0);
    }
  }
}
