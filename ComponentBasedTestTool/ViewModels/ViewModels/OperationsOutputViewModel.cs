using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints;
using ExtensionPoints.ImplementedByContext;

namespace ViewModels.ViewModels
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

    public void WriteLine(string text)
    {
      Content += text + Environment.NewLine;
    }
  }
}
