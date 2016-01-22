using System;

namespace ViewModels.ViewModels.Commands
{
  public interface ApplicationContext
  {
    void Invoke(EventHandler eventHandler, object sender);
  }
}