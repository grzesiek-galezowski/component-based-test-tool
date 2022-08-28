using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComponentBasedTestTool.Annotations;
using Core.Maybe;

namespace ViewModels.ViewModels;

public class ScriptOperationsViewModel : INotifyPropertyChanged, IOperationsViewInitialization
{
  private readonly OperationPropertiesViewModel _operationPropertiesViewModel;
  private ObservableCollection<OperationViewModel> _operationViewModels;
  private Maybe<OperationViewModel> _selectedOperation = Maybe<OperationViewModel>.Nothing;

  public ScriptOperationsViewModel(
    OperationPropertiesViewModel operationPropertiesViewModel)
  {
    _operationPropertiesViewModel = operationPropertiesViewModel;
    _operationViewModels = new ObservableCollection<OperationViewModel>();
  }

  public ObservableCollection<OperationViewModel> Operations
  {
    get { return _operationViewModels; }
    set
    {
      _operationViewModels = value;
      OnPropertyChanged();
    }
  }

  public OperationViewModel? SelectedOperation
  {
    get { return _selectedOperation.OrElseDefault(); } //read at app start
    set
    {
      _selectedOperation = value.ToMaybe();
      _selectedOperation.Do(o => o.SetPropertiesOn(_operationPropertiesViewModel));
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

  public void Update()
  {
    _selectedOperation.Do(o => o.SetPropertiesOn(_operationPropertiesViewModel));
  }
}