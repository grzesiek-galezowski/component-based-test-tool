using System;
using System.Threading;
using System.Windows;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components
{
  /// <summary>
  /// Interaction logic for CustomGui.xaml
  /// </summary>
  public partial class CustomGui : Window, OperationContext //bug the state of the button should depend on configure operation
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

    public void Ready()
    {
      Dispatcher.Invoke(() =>
      {
        sleepButton.IsEnabled = false;
      });
    }

    public void Success()
    {
      Dispatcher.Invoke(() =>
      {
        sleepButton.IsEnabled = true;
      });

    }

    public void Stopped()
    {
      Dispatcher.Invoke(() =>
      {
        sleepButton.IsEnabled = false;
      });

    }

    public void Failure(Exception exception)
    {
      Dispatcher.Invoke(() =>
      {
        sleepButton.IsEnabled = false;
      });

    }

    public void Initial() 
    {
      Dispatcher.Invoke(() =>
      {
        sleepButton.IsEnabled = false;
      });

    }

    public void NotifyOnCurrentState(string stateName, Runnability runnability, ErrorInfo errorInfo)
    {
      MessageBox.Show("current state");
    }

    public void InProgress(CancellationTokenSource cancellationTokenSource)
    {
      Dispatcher.Invoke(() =>
      {
        sleepButton.IsEnabled = false; //bug doesn't work
      });

    }
  }
}
