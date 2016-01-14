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
    readonly List<KeyValuePair<string, Operation>> _operations = 
      new List<KeyValuePair<string, Operation>>();
    private readonly OutputFactory _outputFactory;
    private List<OperationViewModel> _operationViewModels;

    public ComponentInstanceViewModel(string instanceName, OutputFactory outputFactory)
    {
      _instanceName = instanceName;
      _outputFactory = outputFactory;
    }

    public void Initialize(OperationViewModelFactory operationViewModelFactory)
    {
      _operationViewModels = _operations.Select(
        operationViewModelFactory.CreateOperationViewModel).ToList();
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
      operationsViewModel.AddOperations(_operationViewModels);
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
