using System.Xml.Linq;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ViewModels.ViewModels
{
  public class FileBasedPersistentStorage : TestComponentOperationDestination, PersistentStorage
  {
    private readonly OperationsOutputViewModel _operationsOutputViewModel;
    private readonly XmlConfigurationBuilder _xmlConfigurationBuilder;

    public FileBasedPersistentStorage(OperationsOutputViewModel operationsOutputViewModel)
    {
      _operationsOutputViewModel = operationsOutputViewModel;
      _xmlConfigurationBuilder = new XmlConfigurationBuilder();
    }

    public void AddOperation(string name, Operation operation, string dependencyName)
    {
      _xmlConfigurationBuilder.AddOperationXml(name, operation);
      operation.StoreParameters(this);
    }

    public void AddOperation(string name, Operation operation)
    {
      _xmlConfigurationBuilder.AddOperationXml(name, operation);
      operation.StoreParameters(this);
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
      _xmlConfigurationBuilder.StoreValueXml(name, value);
    }

    public void BeginComponentInstance(string instanceName, TestComponent testComponentInstance)
    {
      _xmlConfigurationBuilder.AddComponentInstance2Xml(instanceName, testComponentInstance);
    }

    public void Save()
    {
      _operationsOutputViewModel.WriteLine("Saving XML: " + _xmlConfigurationBuilder.ToString());
      _xmlConfigurationBuilder.Save();
    }
  }
}