namespace ExtensionPoints.ImplementedByContext.StateMachine
{
  public interface OperationControl
  {
    void RegisterContext(OperationContext context);
    void Start();
    void Stop();
    void DeregisterContext(OperationContext context);
  }
}