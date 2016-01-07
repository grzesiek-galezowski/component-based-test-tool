using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ComponentBasedTestTool.Annotations;
using Components;
using ExtensionPoints;

namespace ComponentBasedTestTool.ViewModels
{
  
  public class OperationsViewModel : INotifyPropertyChanged, TestComponentContext
  {
    private readonly OperationPropertiesViewModel _operationPropertiesViewModel;
    private readonly OutputFactory _outputFactory;
    private readonly List<OperationViewModel> _operationViewModels;
    private OperationViewModel _selectedOperation;

    public OperationsViewModel(OperationPropertiesViewModel operationPropertiesViewModel, OutputFactory outputFactory)
    {
      _operationPropertiesViewModel = operationPropertiesViewModel;
      _outputFactory = outputFactory;
      _operationViewModels = new List<OperationViewModel>();
    }

    public IList<OperationViewModel> Operations => _operationViewModels;

    public OperationViewModel SelectedOperation {
      get { return _selectedOperation; }
      set
      {
        _selectedOperation = value;
        _selectedOperation.SetOperationsOn(_operationPropertiesViewModel);
      }
    }

    #region boilerplate
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    public void AddOperation(string operationName, Operation operation)
    {
      Operations.Add(new OperationViewModel(operationName, operation));
    }

    public OperationsOutput CreateOutFor(string operationName)
    {
      return _outputFactory.CreateOutFor(operationName);
    }
  }
}
