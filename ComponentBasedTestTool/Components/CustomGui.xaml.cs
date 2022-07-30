using System;
using System.ComponentModel;
using System.Windows;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components;

/// <summary>
/// Interaction logic for CustomGui.xaml
/// </summary>
public partial class CustomGui : Window
{
  private readonly OperationControl _wait;
  private readonly OperationControl _configure;
  private readonly OperationContext _configureControls;
  private readonly OperationContext _waitControls;

  public CustomGui(OperationControl wait, OperationControl configure)
  {
    InitializeComponent();
    _wait = wait;
    _configure = configure;
    _configureControls = new ConfigurationControls(this);
    _waitControls = new WaitControls(this);
    _configure.RegisterContext(_configureControls);
    _wait.RegisterContext(_waitControls);

  }

  private void button_Click(object sender, RoutedEventArgs e)
  {
    _wait.Start();
  }

  private void Window_Closing(object sender, CancelEventArgs e)
  {
    _configure.DeregisterContext(_configureControls);
    _wait.RegisterContext(_waitControls);
  }

  private void Window_Closed(object sender, EventArgs e)
  {
  }

  public void Enable()
  {
    Dispatcher.Invoke(() =>
    {
      sleepButton.IsEnabled = true;
    });
  }

  public void Disable()
  {
    this.Dispatcher.Invoke(() =>
    {
      sleepButton.IsEnabled = false;
    });
  }

}