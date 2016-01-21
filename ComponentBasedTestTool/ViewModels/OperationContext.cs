using System;

namespace ViewModels
{
  public interface OperationContext
  {
    void Ready();
    void Success();
    void Stopped();
    void Failure(Exception exception);
    void InProgress();
    void Initial();

    void NotifyonCurrentState(
      bool canRun, 
      bool canStop, 
      string initial, 
      string lastErrorFullText, 
      string lastError);
  }
}