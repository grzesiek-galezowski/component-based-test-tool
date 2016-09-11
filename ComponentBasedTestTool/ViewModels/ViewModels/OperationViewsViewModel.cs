using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ComponentBasedTestTool.Annotations;

namespace ViewModels.ViewModels
{
  public class OperationViewsViewModel : INotifyPropertyChanged
  {
    
    private OperationsViewInitialization _selectedItem;

    public OperationsViewInitialization SelectedView
    {
      //get { return _selectedItem; } 
      set { _selectedItem = value;
        Application.Current.Dispatcher.InvokeAsync(() => MessageBox.Show("lol"));
      }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }

}