using System;

namespace ComponentBasedTestTool.Views.Ports
{
  public interface ApplicationContext
  {
    void Invoke(EventHandler eventHandler, object sender);
  }

  public interface ApplicationBootstrap
  {
    //TODO convert to empty interfaces
    void SetComponentInstancesViewDataContext(object componentInstancesViewModel);
    void SetComponentsViewDataContext(object componentsViewModel);
    void SetOperationsOutputViewDataContext(object operationsOutputViewModel);
    void SetOperationsViewDataContext(object operationsViewModel);
    void SetOperationPropertiesViewDataContext(object operationPropertiesViewModel);
    void Start();
    void SetTopMenuBarContext(object topMenuBarContextViewModel);
  }

}