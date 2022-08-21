using System;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components.FileSystem;

public class WaitControls : IOperationContext
{
  private readonly CustomGui _customGui;

  public WaitControls(CustomGui customGui)
  {
    _customGui = customGui;
  }

  public void Ready()
  {
    Enable();
  }

  private void Enable()
  {
    _customGui.Enable();
  }

  private void Disable()
  {
    _customGui.Disable();
  }

  public void Success()
  {
    Enable();
  }

  public void Stopped()
  {
    Enable();
  }

  public void Failure(Exception exception)
  {
    Enable();
  }

  public void InProgress(CancellationTokenSource cancellationTokenSource)
  {
    Disable();
  }



  public void Initial()
  {
    Disable();
  }

  public void NotifyOnCurrentState(string stateName, Runnability runnability, ErrorInfo errorInfo)
  {

  }
}