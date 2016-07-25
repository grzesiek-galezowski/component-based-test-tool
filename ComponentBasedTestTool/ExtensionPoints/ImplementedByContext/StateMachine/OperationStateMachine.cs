namespace ExtensionPoints.ImplementedByContext.StateMachine
{
  public interface OperationStateMachine : OperationSignals, OperationStateObserver
  {
    void InitializeParameters(OperationParametersListBuilder operationParametersListBuilder);
    void SaveUsing(PersistentStorage persistentStorage, string name, ConfigurationOutputBuilder builder);
    void SetContext(OperationContext context);
  }

}
