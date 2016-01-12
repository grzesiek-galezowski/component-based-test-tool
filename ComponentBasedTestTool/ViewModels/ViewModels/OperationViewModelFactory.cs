using System.Collections.Generic;
using ExtensionPoints;
using ViewModels.ViewModels;
using ViewModels.ViewModels.Commands;

public class OperationViewModelFactory
{
  private readonly ApplicationContext _applicationContext;

  public OperationViewModelFactory(ApplicationContext applicationContext)
  {
    _applicationContext = applicationContext;
  }

  public OperationViewModel CreateOperationViewModel(KeyValuePair<string, Operation> o)
  {
    return new OperationViewModel(
      o.Key, 
      o.Value, new OperationCommandFactory(_applicationContext));
  }
}