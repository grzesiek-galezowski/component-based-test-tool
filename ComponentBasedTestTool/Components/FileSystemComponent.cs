using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using ComponentBasedTestTool.ViewModels.Ports;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components
{
  public class FileSystemComponent : TestComponent
  {
    private OperationStateMachine _ls;
    private OperationStateMachine _cs;
    private OperationStateMachine _cat;
    private OperationStateMachine _wait;
    private readonly string _configureName = "configure";
    private OperationStateMachine _configure;
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
      _ls = ctx.CreateOperation(new LsOperation(ctx.CreateOutFor(LsName)));
      _cs = ctx.CreateOperation(new CdOperation(ctx.CreateOutFor(CdName)));
      _cat = ctx.CreateOperation(new CatOperation(ctx.CreateOutFor(CatName)));
      _wait = ctx.CreateOperation(new WaitOperation(ctx.CreateOutFor(SleepName)));
      _configure = ctx.CreateOperation(new ConfigureOperation(ctx.CreateOutFor(_configureName)));
    }

    public void ShowCustomUi()
    {
        var option = MessageBox.Show("lol", "la", MessageBoxButton.YesNoCancel);
        if (option == MessageBoxResult.Yes)
        {
          Dispatcher.CurrentDispatcher.Invoke(() =>
          {
            _wait.Start(new MyOperationContext());
          });
        };
    }
  }

  public class MyOperationContext : OperationContext
  {
    public void Ready()
    {
      throw new System.NotImplementedException();
    }

    public void Success()
    {
      throw new System.NotImplementedException();
    }

    public void Stopped()
    {
      throw new System.NotImplementedException();
    }

    public void Failure(Exception exception)
    {
      throw new System.NotImplementedException();
    }

    public void InProgress(CancellationTokenSource cancellationTokenSource)
    {
      throw new System.NotImplementedException();
    }

    public void Initial()
    {
      throw new System.NotImplementedException();
    }

    public void NotifyOnCurrentState(string stateName, Runnability runnability, ErrorInfo errorInfo)
    {
      throw new System.NotImplementedException();
    }

  }
}