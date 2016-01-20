using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModels.ViewModels;

namespace ComponentBasedTestTool.Views
{
  /// <summary>
  /// Interaction logic for ComponentsView.xaml
  /// </summary>
  public partial class ComponentsView : UserControl
  {
    public ComponentsView()
    {
      InitializeComponent();
    }

    public void SetComponentInstancesViewDataContext(ComponentInstancesViewModel componentInstancesViewModel)
    {
      ComponentInstancesView.DataContext = componentInstancesViewModel;
    }
  }
}
