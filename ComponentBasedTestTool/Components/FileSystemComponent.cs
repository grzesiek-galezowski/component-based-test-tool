using System.Windows;
using System.Windows.Threading;
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
      try
      {
        _customGui = new CustomGui(_wait);
        _configure.RegisterContext(_customGui);
        _customGui.Show();
      }
      finally
      {
        _configure.DeregisterContext(_customGui);
      }
    }
  }
}