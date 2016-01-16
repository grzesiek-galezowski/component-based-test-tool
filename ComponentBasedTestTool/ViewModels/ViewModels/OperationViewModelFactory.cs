using System.Collections.Generic;
using ExtensionPoints;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels
{
  public class OperationViewModelFactory
  {
    private readonly ApplicationContext _applicationContext;

    public OperationViewModelFactory(ApplicationContext applicationContext)
    {
      _applicationContext = applicationContext;
    }

    public OperationViewModel CreateOperationViewModel(OperationEntry o)
    {
      return new OperationViewModel(
        o.Name, 
        o.Operation,
        o.DependencyName, 
        new OperationCommandFactory(_applicationContext));
    }
  }
}