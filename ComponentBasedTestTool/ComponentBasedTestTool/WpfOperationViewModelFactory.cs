using System.Threading;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
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
      var operationViewModel = new OperationViewModel(
        o.Name,
        o.DependencyName, 
        new OperationCommandFactory(_applicationContext), 
        operationPropertiesViewModelBuilder, new DefaultOperationStateMachine(
          o.Operation,
          new NotExecutableOperationState(),
          new CancellationTokenSource()));
      return operationViewModel;
    }
  }
}