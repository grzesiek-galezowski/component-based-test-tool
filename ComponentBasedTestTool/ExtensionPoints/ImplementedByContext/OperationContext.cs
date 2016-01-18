using System;

namespace ExtensionPoints.ImplementedByContext
{
  public interface OperationContext
  {
    void Ready();
    void Success();
    void Stopped();
    void Failure(Exception exception);
    void InProgress();
    void Initial();
  }
}