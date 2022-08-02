namespace ExtensionPoints.ImplementedByContext.StateMachine;

public interface IOperationControl
{
  void RegisterContext(IOperationContext context);
  void Start();
  void Stop();
  void DeregisterContext(IOperationContext context);
}