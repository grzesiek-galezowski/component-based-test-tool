using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CallMeMaybe;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints;

namespace ViewModels.ViewModels
{
  public class ComponentInstanceViewModel : INotifyPropertyChanged, TestComponentContext
  {
    private string _instanceName;
    private readonly OutputFactory _outputFactory;
    private OperationViewModels _operationViewModels;
    private readonly OperationEntries _operationEntries;

    public ComponentInstanceViewModel(
      string instanceName, 
      OutputFactory outputFactory, 
      OperationEntries operationEntries)
    {
      _instanceName = instanceName;
      _outputFactory = outputFactory;
      _operationEntries = operationEntries;
    }

    public void Initialize(OperationViewModelFactory operationViewModelFactory)
    {
      _operationViewModels = _operationEntries.ConvertUsing(operationViewModelFactory);
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
      _operationViewModels.AddTo(operationsViewModel);
    }

    public void AddOperation(string name, Operation operation, string dependencyName)
    {
      _operationEntries.Add(name, operation, Maybe.From(dependencyName));
    }

    public void AddOperation(string name, Operation operation)
    {
      _operationEntries.Add(name, operation, Maybe.Not);
    }

    public OperationsOutput CreateOutFor(string operationName)
    {
      return _outputFactory.CreateOutFor(operationName);
    }

  }

}
