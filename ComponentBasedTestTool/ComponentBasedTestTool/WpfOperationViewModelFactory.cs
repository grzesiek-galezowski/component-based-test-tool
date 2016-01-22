using System.Collections.Generic;
using System.Threading;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ExtensionPoints.ImplementedByComponents;
using ViewModels.ViewModels;
using ViewModels.ViewModels.Commands;

namespace ComponentBasedTestTool
{
  public class WpfOperationViewModelFactory : OperationViewModelFactory
  {
    private readonly ApplicationContext _applicationContext;

    public WpfOperationViewModelFactory(ApplicationContext applicationContext)
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