using System;

namespace ComponentBasedTestTool.ViewModels.Ports
{
  public interface OperationContext
  {
    void Ready();
    void Success();
    void Stopped();
    void Failure(Exception exception);
    void InProgress();
    void Initial();

    void NotifyonCurrentState(string stateName, Runnability runnability, ErrorInfo errorInfo);
  }
}