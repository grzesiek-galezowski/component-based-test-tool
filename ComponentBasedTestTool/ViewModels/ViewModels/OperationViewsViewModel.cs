using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ComponentBasedTestTool.Annotations;

namespace ViewModels.ViewModels
{
  public class OperationViewsViewModel : INotifyPropertyChanged
  {
    private readonly OperationsViewInitialization[] _operationsViewInitializations;
    private OperationsViewInitialization _selectedItem;

    public OperationViewsViewModel(OperationsViewInitialization[] operationsViewInitializations)
    {
      _operationsViewInitializations = operationsViewInitializations;
      _selectedItem = operationsViewInitializations.First();
    }

    public int SelectedIndex
    {
      set
      {
        _selectedItem = _operationsViewInitializations[value];
        _selectedItem.Update();
      }
    }

    public void UpdateForNewComponent()
    {
      _selectedItem.Update();
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