using System.Collections.Generic;
using System.Threading;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;
using ViewModels.ViewModels.Commands;
using ViewModels.ViewModels.OperationStates;

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
      var operationPropertiesViewModelBuilder = 
        new OperationPropertiesViewModelBuilder(o.Name);
      o.Operation.FillParameters(operationPropertiesViewModelBuilder);
      Operation operation = o.Operation;
      var operationViewModel = new OperationViewModel(
        o.Name,
        o.DependencyName, 
        new OperationCommandFactory(_applicationContext), 
        operationPropertiesViewModelBuilder, new DefaultOperationStateMachine(
          operation,
          new NotExecutableOperationState(),
          new List<OperationDependencyObserver>(),
          new CancellationTokenSource()));
      return operationViewModel;
    }
  }
}