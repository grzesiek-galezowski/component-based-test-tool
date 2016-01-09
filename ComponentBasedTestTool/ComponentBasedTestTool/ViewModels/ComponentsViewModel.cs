using System.Collections.Generic;
using System.Windows.Input;
using ComponentBasedTestTool.ViewModels.Commands;

namespace ComponentBasedTestTool.ViewModels
{
  public class ComponentsViewModel
  {
    public IEnumerable<TestComponentViewModel> TestComponents
    {
      get
      {
        return new List<TestComponentViewModel>()
        {
          new TestComponentViewModel() { Name = "Filesystem"},
          new TestComponentViewModel() { Name = "LRRP"},
          new TestComponentViewModel() { Name = "SNMP"},
          new TestComponentViewModel() { Name = "REST"},
          new TestComponentViewModel() { Name = "Very very long name that would fit?"}
        };
      }
    }
  }

  public class TestComponentViewModel
  {
    public ICommand AddComponentCommand => new AddComponentCommand();
    public string Name { get; set; }
  }
}