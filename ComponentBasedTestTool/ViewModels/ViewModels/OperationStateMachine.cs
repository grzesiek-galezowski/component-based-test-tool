using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
  public interface OperationStateMachine
  {
    void DependencyFulfilled(OperationContext operationViewModel);
    void Failure(Exception exception, OperationContext operationViewModel);
    void FromNowOnReportSuccessfulExecutionTo(OperationDependencyObserver observer);
    void Initial(OperationContext observer);
    void InProgress(OperationContext operationViewModel);
    void Ready(OperationContext context);
    void Start(OperationContext context);
    void Stopped(OperationContext operationViewModel);
    void Success(OperationContext operationViewModel);
    void Stop();
  }
}
