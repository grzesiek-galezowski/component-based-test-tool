using System.Linq;
using NUnit.Framework;
using ViewModels.ViewModels;

namespace ComponentSpecification
{
  public class FakeInstancesView
  {
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;

    public FakeInstancesView(ComponentInstancesViewModel componentInstancesViewModel)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
    }

    public void AssertShowsExactlyTheFollowingInstances(string componentName, int expectedCount)
    {
      var count = _componentInstancesViewModel.ComponentInstances
        .Count(c => c.InstanceName == componentName);
      Assert.AreEqual(
        expectedCount,
        count, $"Instances count of component {componentName} is not as expected");
    }
  }
}