using ExtensionPoints.ImplementedByComponents;

namespace ComponentBasedTestTool.Domain;

public interface IConfigurationOutputBuilder
{
  void AppendOperationNode(string name, IRunnable operation);
  void AppendProperty<T>(string name, T value);
  void Save();
  void AppendComponentInstanceNode(string instanceName, ICoreTestComponent testComponentInstance);
}