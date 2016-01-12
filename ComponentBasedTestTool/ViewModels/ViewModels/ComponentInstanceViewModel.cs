using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints;

namespace ViewModels.ViewModels
{
  public class ComponentInstanceViewModel : INotifyPropertyChanged, TestComponentContext
  {
    private string _instanceName;
    readonly List<KeyValuePair<string, Operation>> _operations = new List<KeyValuePair<string, Operation>>();
    private readonly OutputFactory _outputFactory;
    private readonly OperationViewModelFactory _operationViewModelFactory;

    public ComponentInstanceViewModel(string instanceName, TestComponent testComponentInstance, OutputFactory outputFactory, OperationViewModelFactory operationViewModelFactory)
    {
      _instanceName = instanceName;
      _outputFactory = outputFactory;
      testComponentInstance.PopulateOperations(this);
      _operationViewModelFactory = operationViewModelFactory;
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
      var operationViewModels = _operations.Select(o => 
        _operationViewModelFactory.CreateOperationViewModel(o)
        ).ToList();
      operationsViewModel.AddOperations(operationViewModels);
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
