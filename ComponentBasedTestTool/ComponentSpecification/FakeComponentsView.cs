using System.Linq;
using NUnit.Framework;
using ViewModels.ViewModels;

namespace ComponentSpecification
{
  public class FakeComponentsView
  {
    private readonly ComponentsViewModel _componentsViewModel;

    public FakeComponentsView(ComponentsViewModel componentsViewModel)
    {
      _componentsViewModel = componentsViewModel;
    }

    public void AssertExactlyTheFollowingAreLoaded(params string[] names)
    {
      Assert.AreEqual(names.Count(), _componentsViewModel.TestComponents.Count);
      foreach (var name in names)
      {
        Assert.True(_componentsViewModel.TestComponents.Any(c => c.Name == name),
          "could not find any component by the name " + name);
      }
    }

    public void AddInstanceOf(string componentName)
    {
      var fakeTestComponent = FakeTestComponent.GetByName(componentName, _componentsViewModel);
      fakeTestComponent.AddInstance();
    }
  }
}