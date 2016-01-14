using System.Threading;
using System.Threading.Tasks;

namespace ExtensionPoints
{
  public interface Operation
  {
    Task RunAsync(CancellationToken token);
    void FillParameters(OperationParametersListBuilder parameters);
  }
}