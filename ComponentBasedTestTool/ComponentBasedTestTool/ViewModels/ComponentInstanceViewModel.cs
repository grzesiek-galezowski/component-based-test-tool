using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints;

namespace ComponentBasedTestTool.ViewModels
{
  public class ComponentInstanceViewModel : INotifyPropertyChanged, TestComponentContext
  {
    private readonly TestComponent _testComponentInstance;
    private string _instanceName;
    readonly List<KeyValuePair<string, Operation>> _operations = new List<KeyValuePair<string, Operation>>();
    private readonly OutputFactory _outputFactory;

    public ComponentInstanceViewModel(string instanceName, TestComponent testComponentInstance, OutputFactory outputFactory)
    {
      _instanceName = instanceName;
      _testComponentInstance = testComponentInstance;
      _outputFactory = outputFactory;
      _testComponentInstance.PopulateOperations(this);
    }

    public string InstanceName
    {
      get { return _instanceName; }
      set
      {
        _instanceName = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void AddOperationsTo(OperationsViewModel operationsViewModel)
    {
      operationsViewModel.AddOperations(_operations.Select(o => new OperationViewModel(o.Key, o.Value)).ToList());
    }

    public void AddOperation(string name, Operation operation)
    {
      _operations.Add(new KeyValuePair<string, Operation>(name, operation));
    }

    public OperationsOutput CreateOutFor(string operationName)
    {
      return _outputFactory.CreateOutFor(operationName);
    }

  }
}
