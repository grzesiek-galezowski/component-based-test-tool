using System.Collections.Generic;
using ComponentBasedTestTool.Domain;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ViewModels.ViewModels
{
  public class OperationMachinesByControlObject
  {
    private readonly Dictionary<OperationControl, OperationStateMachine> _operationMachinesByPluginView;

    public OperationMachinesByControlObject()
    {
      _operationMachinesByPluginView = new Dictionary<OperationControl, OperationStateMachine>();
    }

    public OperationStateMachine For(OperationControl operation)
    {
      return _operationMachinesByPluginView[operation];
    }

    public void Register(OperationStateMachine defaultOperationStateMachine)
    {
      _operationMachinesByPluginView[defaultOperationStateMachine] = defaultOperationStateMachine;
    }
  }
}