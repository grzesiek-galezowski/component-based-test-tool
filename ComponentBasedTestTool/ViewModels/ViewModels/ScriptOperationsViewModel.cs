﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CallMeMaybe;
using ComponentBasedTestTool.Annotations;

namespace ViewModels.ViewModels
{
  public class ScriptOperationsViewModel : INotifyPropertyChanged, OperationsViewInitialization
  {
    private readonly OperationPropertiesViewModel _operationPropertiesViewModel;
    private ObservableCollection<OperationViewModel> _operationViewModels;
    private Maybe<OperationViewModel> _selectedOperation = Maybe<OperationViewModel>.Not;

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

    public OperationViewModel SelectedOperation
    {
      get { return _selectedOperation.Single(); }
      set
      {
        _selectedOperation = value;
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
}