using System;

namespace ExtensionPoints
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