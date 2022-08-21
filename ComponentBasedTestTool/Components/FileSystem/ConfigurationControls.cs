using System;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components.FileSystem;

public class ConfigurationControls : IOperationContext
{
  private readonly CustomGui _customGui;

  public ConfigurationControls(CustomGui customGui)
  {
    _customGui = customGui;
  }


  public void Ready()
  {

  }

  public void Success()
  {
    _customGui.Enable();
  }

  public void Stopped()
  {
    _customGui.Enable();
  }


  public void Initial()
  {
    _customGui.Disable();
  }

  public void NotifyOnCurrentState(string stateName, Runnability runnability, ErrorInfo errorInfo)
  {

  }

  public void InProgress(CancellationTokenSource cancellationTokenSource)
  {
    _customGui.Disable();
  }

  public void Failure(Exception exception)
  {
    _customGui.Enable();
  }
}