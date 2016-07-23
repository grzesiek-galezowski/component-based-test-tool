using System.Xml.Linq;
using ComponentBasedTestTool.Domain;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ViewModels.ViewModels
{
  public class FileBasedPersistentStorage : TestComponentOperationDestination, PersistentStorage
  {
    private readonly OperationsOutputViewModel _operationsOutputViewModel;
    private readonly XmlConfigurationOutputBuilder _xmlConfigurationOutputBuilder;

    public FileBasedPersistentStorage(OperationsOutputViewModel operationsOutputViewModel)
    {
      _operationsOutputViewModel = operationsOutputViewModel;
      _xmlConfigurationOutputBuilder = new XmlConfigurationOutputBuilder();
    }

    public void AddOperation(string name, OperationStateMachine operation, string dependencyName)
    {
      operation.SaveUsing(this, name, _xmlConfigurationOutputBuilder); //bug remove name from here and pass through constructor
    }

    public void AddOperation(string name, OperationStateMachine operation)
    {
      operation.SaveUsing(this, name, _xmlConfigurationOutputBuilder); //bug remove name from here and pass through constructor
    }

    public void Store(params Persistable[] persistables)
    {
      foreach (var persistable in persistables)
      {
        persistable.StoreIn(this);
      }
      
    }

    public void StoreValue<T>(string name, T value)
    {
      _xmlConfigurationOutputBuilder.AppendProperty(name, value);
    }

    public void NewComponentInstance(string instanceName, TestComponent testComponentInstance)
    {
      _xmlConfigurationOutputBuilder.AppendComponentInstanceNode(instanceName, testComponentInstance);
    }

    public void Save()
    {
      _operationsOutputViewModel.WriteLine("Saving XML: " + _xmlConfigurationOutputBuilder.ToString());
      _xmlConfigurationOutputBuilder.Save();
    }
  }
}