using ViewModels.ViewModels;
using Xunit;

namespace ComponentSpecification.AutomationLayer;

public class FakeComponentsView
{
  private readonly ComponentsViewModel _componentsViewModel;

  public FakeComponentsView(ComponentsViewModel componentsViewModel)
  {
    _componentsViewModel = componentsViewModel;
  }

  public void AssertExactlyTheFollowingAreLoaded(params string[] names)
  {
    Assert.Equal(names.Count(), _componentsViewModel.TestComponents.Count);
    foreach (var name in names)
    {
      Assert.True(_componentsViewModel.TestComponents.Any(c => c.Name.StartsWith(name)),
        "could not find any component by the name " + name);
    }
  }

  public void AddInstanceOf(string componentName)
  {
    var fakeTestComponent = FakeTestComponent.GetByName(componentName, _componentsViewModel);
    fakeTestComponent.AddInstance();
  }
}