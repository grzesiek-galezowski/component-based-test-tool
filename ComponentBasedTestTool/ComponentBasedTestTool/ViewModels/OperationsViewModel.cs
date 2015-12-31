using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.ViewModels.OperationStates;

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

  public class CatOperation : Operation
  {
    private readonly OperationsOutputViewModel _operationsOutputViewModel;

    public CatOperation(OperationsOutputViewModel operationsOutputViewModel)
    {
      _operationsOutputViewModel = operationsOutputViewModel;
    }

    public async Task RunAsync()
    {
      _operationsOutputViewModel.Content += "cat Folder" + Environment.NewLine;
    }
  }

  public class CdOperation : Operation
  {
    private readonly OperationsOutputViewModel _operationsOutputViewModel;

    public CdOperation(OperationsOutputViewModel operationsOutputViewModel)
    {
      _operationsOutputViewModel = operationsOutputViewModel;
    }

    public async Task RunAsync()
    {
      _operationsOutputViewModel.Content += "cd lolek" + Environment.NewLine;
    }
  }

  public class LsOperation : Operation
  {
    private readonly OperationsOutputViewModel _operationsOutputViewModel;

    public LsOperation(OperationsOutputViewModel operationsOutputViewModel)
    {
      _operationsOutputViewModel = operationsOutputViewModel;
    }

    public async Task RunAsync()
    {
      _operationsOutputViewModel.Content += "lolki2" + Environment.NewLine;
      await Task.Delay(2000);
      _operationsOutputViewModel.Content += "123123123" + Environment.NewLine;
    }
  }
}
