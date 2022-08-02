using System.Windows;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace Components.FileSystem;

public class FileSystemComponent :
  ICoreTestComponent,
  Capabilities.ICustomGui,
  Capabilities.ICleanupOnEnvironmentClosing
{
  private IOperationControl _ls;
  private IOperationControl _cs;
  private IOperationControl _cat;
  private IOperationControl _wait;
  private IOperationControl _configure;
  private readonly string _configureName = "configure";
  private CustomGui _customGui;

  private const string SleepName = "sleep";
  private const string CatName = "cat";
  private const string CdName = "cd";
  private const string LsName = "ls";

  public void PopulateOperations(ITestComponentOperationDestination ctx)
  {
    ctx.AddOperation(_configureName, _configure);
    ctx.AddOperation(LsName, _ls, _configureName);
    ctx.AddOperation(CdName, _cs, _configureName);
    ctx.AddOperation(CatName, _cat, _configureName);
    ctx.AddOperation(SleepName, _wait, _configureName);
  }

  public void CreateOperations(ITestComponentContext ctx)
  {
    _ls = ctx.CreateOperation(new LsOperation(ctx.CreateOutFor(LsName)));
    _cs = ctx.CreateOperation(new CdOperation(ctx.CreateOutFor(CdName)));
    _cat = ctx.CreateOperation(new CatOperation(ctx.CreateOutFor(CatName)));
    _wait = ctx.CreateOperation(new WaitOperation(ctx.CreateOutFor(SleepName)));
    _configure = ctx.CreateOperation(new ConfigureOperation(ctx.CreateOutFor(_configureName)));
  }

  public void ShowCustomUi()
  {
    if (_customGui is not { IsLoaded: true })
    {
      _customGui = new CustomGui(_wait, _configure)
      {
        Owner = Application.Current.MainWindow
      };
      _customGui.Show();
    }
    else
    {
      _customGui.Focus();
    }
  }

  public void CleanupOnClosing()
  {
    MessageBox.Show("Cleaning up!");
  }
}