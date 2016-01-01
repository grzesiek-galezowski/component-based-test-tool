using System.Threading.Tasks;

namespace ComponentBasedTestTool.ViewModels.OperationStates
{
  public interface Operation
  {
    Task RunAsync();
  }
}