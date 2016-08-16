using System.Linq;
using FluentAssertions;
using ViewModels.ViewModels;
using Xunit;

namespace ComponentSpecification.AutomationLayer
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
      
      expectedCount.Should().Be(count, $"Instances count of component {componentName} is not as expected");
    }

    public void Select(string name)
    {
      _componentInstancesViewModel.SelectedInstance = InstanceWith(name);
    }

    private ComponentInstanceViewModel InstanceWith(string name)
    {
      return _componentInstancesViewModel.ComponentInstances.First(i => i.InstanceName == name);
    }

    public void ShowCustomGui(string name)
    {
      InstanceWith(name).ShowCustomUiForComponentInstanceCommand.Execute(null);
    }
  }
}