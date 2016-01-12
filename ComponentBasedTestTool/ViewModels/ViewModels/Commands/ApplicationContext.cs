using ComponentBasedTestTool.Annotations;
using System;

namespace ViewModels.ViewModels.Commands
{
  public interface ApplicationContext
  {
    void Invoke([CanBeNull] EventHandler eventHandler, object sender);
  }
}