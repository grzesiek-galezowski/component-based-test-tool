using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ComponentBasedTestTool.Annotations;

namespace ComponentBasedTestTool.ViewModels
{
  
  public class OperationsViewModel : INotifyPropertyChanged
  {
    private readonly OperationParametersViewModel _operationParametersViewModel;
    private readonly List<OperationViewModel> _operationViewModels;
    private OperationViewModel _selectedOperation;

    public OperationsViewModel(OperationParametersViewModel operationParametersViewModel)
    {
      _operationParametersViewModel = operationParametersViewModel;
      _operationViewModels = new List<OperationViewModel>();
    }

    public IList<OperationViewModel> Operations => _operationViewModels;

    public OperationViewModel SelectedOperation {
      get { return _selectedOperation; }
      set
      {
        _selectedOperation = value;
        _selectedOperation.SetOperationsOn(_operationParametersViewModel);
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
  }
}
