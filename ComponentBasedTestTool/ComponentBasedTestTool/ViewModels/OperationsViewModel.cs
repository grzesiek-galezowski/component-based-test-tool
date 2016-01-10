﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ComponentBasedTestTool.Annotations;
using Components;

namespace ComponentBasedTestTool.ViewModels
{
  
  public class OperationsViewModel : INotifyPropertyChanged
  {
    private readonly OperationPropertiesViewModel _operationPropertiesViewModel;
    private ObservableCollection<OperationViewModel> _operationViewModels;
    private OperationViewModel _selectedOperation;

    public OperationsViewModel(
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

    public OperationViewModel SelectedOperation {
      get { return _selectedOperation; }
      set
      {
        _selectedOperation = value;
        _selectedOperation.SetPropertiesOn(_operationPropertiesViewModel);
      }
    }

    public void AddOperations(List<OperationViewModel> operationViewModels)
    {
      _operationPropertiesViewModel.ClearPropertiesList();
      Operations = new ObservableCollection<OperationViewModel>(operationViewModels);
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
