using System;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components
{
  public class FileSystemComponent : TestComponent
  {
    private OperationControl _ls;
    private OperationControl _cs;
    private OperationControl _cat;
    private OperationControl _wait;
    private OperationControl _configure;
    private readonly string _configureName = "configure";
    private CustomGui _customGui;

    public FileSystemComponent()
    {
    }

    private const string SleepName = "sleep";
    private const string CatName = "cat";
    private const string CdName = "cd";
    private const string LsName = "ls";

    public void PopulateOperations(TestComponentOperationDestination ctx)
    {
      ctx.AddOperation(_configureName, _configure);
      ctx.AddOperation(LsName, _ls, _configureName);
      ctx.AddOperation(CdName, _cs, _configureName);
      ctx.AddOperation(CatName, _cat, _configureName);
      ctx.AddOperation(SleepName, _wait, _configureName);
    }

    public void CreateOperations(TestComponentContext _)
    {
      _ls = _.CreateOperation(new LsOperation(_.CreateOutFor(LsName)));
      _cs = _.CreateOperation(new CdOperation(_.CreateOutFor(CdName)));
      _cat = _.CreateOperation(new CatOperation(_.CreateOutFor(CatName)));
      _wait = _.CreateOperation(new WaitOperation(_.CreateOutFor(SleepName)));
      _configure = _.CreateOperation(new ConfigureOperation(_.CreateOutFor(_configureName)));

    }

    public void ShowCustomUi()
    {
      _customGui = new CustomGui(_wait, _configure);
    
      _customGui.Show();

      MessageBox.Show("end show");
    }
  }

  public class WaitControls : OperationContext
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