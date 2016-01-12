using System;
using System.Threading.Tasks;
using ExtensionPoints;

namespace ViewModels.ViewModels.OperationStates
{
  public sealed class ExecutableOperationState : OperationState
  {
    public void Run(OperationViewModel operationViewModel, Operation operation)
    {
      Task.Run(async () =>
      {
        try
        {
          operationViewModel.InProgress();
          await operation.RunAsync();
          operationViewModel.Success();
        }
        catch (Exception e)
        {
          operationViewModel.Failure(e);
        }
      });
    }
  }
}