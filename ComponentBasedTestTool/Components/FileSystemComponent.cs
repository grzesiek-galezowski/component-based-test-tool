using System.Threading;
using System.Windows;
using System.Windows.Threading;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components
{
  public class FileSystemComponent : TestComponent
  {
    private LsOperation _ls;
    private CdOperation _cs;
    private CatOperation _cat;
    private WaitOperation _wait;
    private string _configureName = "configure";
    private ConfigureOperation _configure;
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

    public void CreateOperations(TestComponentContext ctx)
    {
      _ls = new LsOperation(ctx.CreateOutFor(LsName));
      _cs = new CdOperation(ctx.CreateOutFor(CdName));
      _cat = new CatOperation(ctx.CreateOutFor(CatName));
      _wait = new WaitOperation(ctx.CreateOutFor(SleepName));
      _configure = new ConfigureOperation(ctx.CreateOutFor(_configureName));
    }

    public void ShowCustomUi()
    {
        var option = MessageBox.Show("lol", "la", MessageBoxButton.YesNoCancel);
        if (option == MessageBoxResult.Yes)
        {
          Dispatcher.CurrentDispatcher.InvokeAsync(async () =>
          {
            await _wait.RunAsync(new CancellationToken());
          });
        };
    }
  }
}