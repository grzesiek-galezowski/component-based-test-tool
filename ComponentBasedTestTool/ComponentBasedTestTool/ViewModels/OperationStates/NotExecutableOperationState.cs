using System;
using ExtensionPoints;

namespace ComponentBasedTestTool.ViewModels.OperationStates
{
  public class NotExecutableOperationState : OperationState
  {
    public void Run(OperationViewModel operationViewModel, Operation operation)
    {
      throw new NotSupportedException("The operation cannot be invoked right now");
    }
  }
}