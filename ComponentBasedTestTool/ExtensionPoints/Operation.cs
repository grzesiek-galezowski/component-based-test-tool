using System.Threading.Tasks;

namespace ExtensionPoints
{
  public interface Operation
  {
    Task RunAsync();
    void FillParameters(OperationParametersListBuilder parameters);
  }
}