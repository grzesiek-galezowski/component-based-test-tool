using System.Collections.Generic;
using ComponentBasedTestTool.Domain;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ViewModels.ViewModels;

public class OperationMachinesByControlObject
{
  private readonly Dictionary<IOperationControl, IOperationStateMachine> _operationMachinesByPluginView;

  public OperationMachinesByControlObject()
  {
    _operationMachinesByPluginView = new Dictionary<IOperationControl, IOperationStateMachine>();
  }

  public IOperationStateMachine For(IOperationControl operation)
  {
    return _operationMachinesByPluginView[operation];
  }

  public void Register(IOperationStateMachine defaultOperationStateMachine)
  {
    _operationMachinesByPluginView[defaultOperationStateMachine] = defaultOperationStateMachine;
  }
}