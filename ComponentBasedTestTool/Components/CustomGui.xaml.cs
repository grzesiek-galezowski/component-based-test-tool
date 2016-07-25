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
using System.Windows.Shapes;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components
{
  /// <summary>
  /// Interaction logic for CustomGui.xaml
  /// </summary>
  public partial class CustomGui : Window
  {
    private readonly OperationControl _wait;

    public CustomGui(OperationControl wait)
    {
      _wait = wait;
      InitializeComponent();
    }

    private void button_Click(object sender, RoutedEventArgs e)
    {
      _wait.Start();
    }
  }
}
