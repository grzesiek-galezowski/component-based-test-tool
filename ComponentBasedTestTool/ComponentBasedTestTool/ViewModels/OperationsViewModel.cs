﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComponentBasedTestTool.Annotations;

namespace ComponentBasedTestTool.ViewModels
{
  
  public class OperationsViewModel : INotifyPropertyChanged
  {
    private readonly List<OperationViewModel> _operationViewModels;

    public OperationsViewModel()
    {
      _operationViewModels = new List<OperationViewModel>();
    }

    public IList<OperationViewModel> Operations => _operationViewModels;

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
