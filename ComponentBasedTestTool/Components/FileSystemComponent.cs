using System.Threading;
using System.Threading.Tasks;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components
{
  public class FileSystemComponent : TestComponent
  {
    private const string SleepName = "sleep";
    private const string CatName = "cat";
    private const string CdName = "cd";
    private const string LsName = "ls";

    public void PopulateOperations(TestComponentContext ctx)
    {
      var ls = new LsOperation(ctx.CreateOutFor(LsName));
      var cd = new CdOperation(ctx.CreateOutFor(CdName));
      var cat = new CatOperation(ctx.CreateOutFor(CatName));
      var wait = new WaitOperation(ctx.CreateOutFor(SleepName));

      var configureName = "configure";
      var configureOperation = new ConfigureOperation(ctx.CreateOutFor(configureName));

      ctx.AddOperation(configureName, configureOperation);
      ctx.AddOperation(LsName, ls, configureName);
      ctx.AddOperation(CdName, cd, configureName);
      ctx.AddOperation(CatName, cat, configureName);
      ctx.AddOperation(SleepName, wait, configureName);
    }
  }

  public class ConfigureOperation : Operation
  {
    private readonly OperationsOutput _output;

    public ConfigureOperation(OperationsOutput output)
    {
      _output = output;
    }

    public Task RunAsync(CancellationToken token)
    {
      return Task.CompletedTask;
    }

    public void FillParameters(OperationParametersListBuilder parameters)
    {
      
    }
  }
}