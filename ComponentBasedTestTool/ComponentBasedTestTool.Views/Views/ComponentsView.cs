using System.Windows.Controls;

namespace ComponentBasedTestTool.Views.Views;

/// <summary>
/// Interaction logic for ComponentsView.xaml
/// </summary>
public partial class ComponentsView
{
  public ComponentsView()
  {
    InitializeComponent();
  }

  public void SetComponentInstancesViewDataContext(object dataContext)
  {
    ComponentInstancesView.DataContext = dataContext;
  }
}