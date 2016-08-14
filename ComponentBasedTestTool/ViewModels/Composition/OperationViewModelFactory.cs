using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentBasedTestTool.Domain;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ViewModels.ViewModels
{
  public interface OperationViewModelFactory
  {
    OperationViewModel CreateOperationViewModel(OperationEntry operationEntry, OperationStateMachine operationStateMachine);
  }


}
