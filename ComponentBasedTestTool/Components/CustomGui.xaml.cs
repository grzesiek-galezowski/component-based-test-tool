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
  public partial class CustomGui : Window, OperationContext
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
      MessageBox.Show("ready");
    }

    public void Success()
    {
      MessageBox.Show("success");
    }

    public void Stopped()
    {
      MessageBox.Show("stopped");
    }

    public void Failure(Exception exception)
    {
      MessageBox.Show("failure");
    }

    public void Initial() //bug when UI is open, initial state must be passed to it.
    {
      MessageBox.Show("initial");
    }

    public void NotifyOnCurrentState(string stateName, Runnability runnability, ErrorInfo errorInfo)
    {
      MessageBox.Show("current state");
    }

    public void InProgress(CancellationTokenSource cancellationTokenSource)
    {
      MessageBox.Show("in progress");
    }
  }
}
