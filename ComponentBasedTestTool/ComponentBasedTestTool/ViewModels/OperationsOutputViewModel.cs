using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints;

namespace ComponentBasedTestTool.ViewModels
{
  public class OperationsOutputViewModel : INotifyPropertyChanged, OperationsOutput
  {
    private string _content = string.Empty;

    public string Content
    {
      get { return _content; }
      set
      {
        _content = value;
        OnPropertyChanged();
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

    public void Write(string str)
    {
      Content += str;
    }
  }
}
