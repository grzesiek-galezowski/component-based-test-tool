using System.Linq;
using ViewModels.ViewModels;

namespace ComponentSpecification.AutomationLayer
{
  public class FakeTestComponent
  {
    private readonly TestComponentViewModel _testComponentViewModel;

    public FakeTestComponent(TestComponentViewModel testComponentViewModel)
    {
      _testComponentViewModel = testComponentViewModel;
    }

    public void AddInstance()
    {
      _testComponentViewModel
        .AddComponentInstanceCommand.Execute(null);
    }

    public static FakeTestComponent GetByName(string componentName, ComponentsViewModel componentsViewModel)
    {
      var testComponentViewModel = componentsViewModel
        .TestComponents
        .First(c => c.Name == componentName);
      var fakeTestComponent = new FakeTestComponent(testComponentViewModel);
      return fakeTestComponent;
    }
  }
}