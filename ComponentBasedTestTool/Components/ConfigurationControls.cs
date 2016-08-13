using System;
using System.Threading;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components
{
  public class ConfigurationControls : OperationContext
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
}