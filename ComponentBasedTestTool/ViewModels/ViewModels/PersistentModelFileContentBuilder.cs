using ComponentBasedTestTool.Domain;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;

namespace ViewModels.ViewModels;

public class PersistentModelFileContentBuilder : ITestComponentOperationDestination, IPersistentStorage
{
  private readonly OperationsOutputViewModel _operationsOutputViewModel;
  private readonly OperationMachinesByControlObject _operationMachinesByControlObject;
  private readonly IConfigurationOutputBuilder _xmlConfigurationOutputBuilder;

  public PersistentModelFileContentBuilder(OperationsOutputViewModel operationsOutputViewModel, OperationMachinesByControlObject operationMachinesByControlObject)
  {
    _operationsOutputViewModel = operationsOutputViewModel;
    _operationMachinesByControlObject = operationMachinesByControlObject;
    _xmlConfigurationOutputBuilder = new XmlConfigurationOutputBuilder();
  }

  public void AddOperation(string name, IOperationControl operation, string dependencyName)
  {
    var stateMachine = _operationMachinesByControlObject.For(operation);
    stateMachine.SaveUsing(this, name, _xmlConfigurationOutputBuilder); //bug remove name from here and pass through constructor
  }

  public void AddOperation(string name, IOperationControl operation)
  {
    var stateMachine = _operationMachinesByControlObject.For(operation);
    stateMachine.SaveUsing(this, name, _xmlConfigurationOutputBuilder); //bug remove name from here and pass through constructor
  }

  public void Store(params IPersistable[] persistables)
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

  public void NewComponentInstance(string instanceName, ICoreTestComponent testComponentInstance)
  {
    _xmlConfigurationOutputBuilder.AppendComponentInstanceNode(instanceName, testComponentInstance);
  }

  public void Save()
  {
    _operationsOutputViewModel.WriteLine("Saving XML: " + _xmlConfigurationOutputBuilder.ToString());
    _xmlConfigurationOutputBuilder.Save();
  }
}