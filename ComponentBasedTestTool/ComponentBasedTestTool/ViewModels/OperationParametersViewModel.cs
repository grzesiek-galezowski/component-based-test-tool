using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ComponentBasedTestTool.Annotations;

namespace ComponentBasedTestTool.ViewModels
{
  public class OperationParametersViewModel : INotifyPropertyChanged
  {
    private readonly List<OperationParameterViewModel> _operationParametersViewModels;

    public OperationParametersViewModel()
    {
      _operationParametersViewModels = new List<OperationParameterViewModel>();
    }

    public IList<OperationParameterViewModel> OperationParameters => _operationParametersViewModels;

    #region boilerplate
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
  }

  public class OperationParameterViewModel
  {
    public string Option { get; set; }
    public string Value { get; set; }
  }
}