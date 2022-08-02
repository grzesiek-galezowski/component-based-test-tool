using System;
using System.ComponentModel;
using System.Windows;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components.FileSystem;

/// <summary>
/// Interaction logic for CustomGui.xaml
/// </summary>
public partial class CustomGui : Window
{
  private readonly IOperationControl _wait;
  private readonly IOperationControl _configure;
  private readonly IOperationContext _configureControls;
  private readonly IOperationContext _waitControls;

  public CustomGui(IOperationControl wait, IOperationControl configure)
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