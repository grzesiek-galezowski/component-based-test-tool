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

namespace ComponentBasedTestTool.Views
{
  /// <summary>
  /// Interaction logic for OperationParametersView.xaml
  /// </summary>
  public partial class OperationParametersView : UserControl
  {
    public OperationParametersView()
    {
      InitializeComponent();
    }
  }

  public class SettingsTemplateSelector : DataTemplateSelector
  {
    public DataTemplate CheckBoxTemplate { get; set; }
    public DataTemplate TextBoxTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      /*
      Setting setting = item as Setting;
      if (setting.IsCheckBox)
      {
        return CheckBoxTemplate;
      }*/
      return TextBoxTemplate;
    }
  }
}
