using ExtensionPoints;

namespace Components
{
  public class FileSystemComponent : TestComponent
  {
    public void PopulateOperations(TestComponentContext ctx)
    {
      ctx.AddOperation("ls", new LsOperation(ctx.CreateOutFor("ls")));
      ctx.AddOperation("cd", new CdOperation(ctx.CreateOutFor("cd")));
      ctx.AddOperation("cat", new CatOperation(ctx.CreateOutFor("cat")));
      ctx.AddOperation("sleep", new WaitOperation(ctx.CreateOutFor("sleep")));
    }
  }

  public class FileSystemComponentFactory : TestComponentFactory
  {
    public TestComponent Create()
    {
      return new FileSystemComponent();
    }
  }
}