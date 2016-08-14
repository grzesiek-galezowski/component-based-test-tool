using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain
{
  public interface OperationStateMachine : OperationSignals, OperationStateObserver
  {
    void InitializeParametersIn(OperationParametersListBuilder operationParametersListBuilder);
    void SaveUsing(PersistentStorage persistentStorage, string name, ConfigurationOutputBuilder builder);
    void RegisterContext(OperationContext context);
  }

}
