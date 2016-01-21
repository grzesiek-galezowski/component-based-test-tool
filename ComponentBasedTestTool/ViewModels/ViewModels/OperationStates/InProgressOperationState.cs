using System.Threading.Tasks;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ViewModels.ViewModels.OperationStates
{
  public class InProgressOperationState : OperationState
  {
    public void Run(OperationContext context, Operation operation)
    {
      
    }

    public void DependencyFulfilled(OperationContext operationViewModel)
    {
      
    }
  }
}