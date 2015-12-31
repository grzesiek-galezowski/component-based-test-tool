using System.Windows.Controls;
using ComponentBasedTestTool.ViewModels;

namespace ComponentBasedTestTool.Views
{
  /// <summary>
  /// Interaction logic for OperationsOutputView.xaml
  /// </summary>
  public partial class OperationsOutputView : UserControl
  {
    public OperationsOutputView()
    {
      DataContext = new OperationsOutputViewModel();
      InitializeComponent();
    }
  }
}
