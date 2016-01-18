using System;
using ComponentBasedTestTool.Annotations;

namespace ViewModels.ViewModels.Commands
{
  public interface ApplicationContext
  {
    void Invoke([CanBeNull] EventHandler eventHandler, object sender);
  }
}