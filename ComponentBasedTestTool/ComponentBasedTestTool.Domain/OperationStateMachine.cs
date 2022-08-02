using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ComponentBasedTestTool.Domain;

public interface IOperationStateMachine : IOperationSignals, IOperationStateObserver
{
  void InitializeParametersIn(IOperationParametersListBuilder operationParametersListBuilder);
  void SaveUsing(IPersistentStorage persistentStorage, string name, IConfigurationOutputBuilder builder);
  void RegisterContext(IOperationContext context);
}