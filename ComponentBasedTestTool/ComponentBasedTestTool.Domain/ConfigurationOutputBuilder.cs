﻿using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain
{
  public interface ConfigurationOutputBuilder
  {
    void AppendOperationNode(string name, Runnable operation);
    void AppendProperty<T>(string name, T value);
    void Save();
    void AppendComponentInstanceNode(string instanceName, CoreTestComponent testComponentInstance);
  }
}
