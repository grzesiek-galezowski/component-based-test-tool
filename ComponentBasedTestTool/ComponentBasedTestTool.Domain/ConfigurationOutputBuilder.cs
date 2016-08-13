﻿using ExtensionPoints.ImplementedByComponents;

{
  public interface ConfigurationOutputBuilder
  {
    void AppendOperationNode(string name, Runnable operation);
    void AppendProperty<T>(string name, T value);
    void Save();
    void AppendComponentInstanceNode(string instanceName, TestComponent testComponentInstance);
  }
}
}