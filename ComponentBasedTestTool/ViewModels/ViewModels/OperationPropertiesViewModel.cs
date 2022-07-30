using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComponentBasedTestTool.Annotations;

namespace ViewModels.ViewModels;

public class OperationPropertiesViewModel : INotifyPropertyChanged
{
  private object _properties = null;

  public object Properties
  {
    get { return _properties; }
    set
    {
      _properties = value;
      OnPropertyChanged();
    }
  }

  public event PropertyChangedEventHandler PropertyChanged;

  [NotifyPropertyChangedInvocator]
  protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  public void ClearPropertiesList()
  {
    this.Properties = null;
  }
}