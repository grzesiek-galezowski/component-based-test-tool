using System.Windows;
using System.Windows.Controls;

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

  public class OperationParameterValueTemplateSelector : DataTemplateSelector
  {
    public DataTemplate ComboBoxTemplate { get; set; }
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
