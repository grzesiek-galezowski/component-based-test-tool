using System.Threading.Tasks;
using ExtensionPoints;

namespace ComponentBasedTestTool.ViewModels.OperationStates
{
  public interface Operation
  {
    Task RunAsync();
    void FillParameters(OperationParametersListBuilder parameters);
  }
}