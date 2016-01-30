using System.Threading;
using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
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

    public OperationViewModel CreateOperationViewModel(OperationEntry operationEntry)
    {
      var operationPropertiesViewModelBuilder = 
        new OperationPropertiesViewModelBuilder(operationEntry.Name);

      operationEntry.FillParameters(operationPropertiesViewModelBuilder);
      operationPropertiesViewModelBuilder.RetrieveList();

      var operationViewModel = OperationViewModelFor(
        operationEntry, 
        operationPropertiesViewModelBuilder, 
        StateMachineFor(operationEntry));
      return operationViewModel;
    }

    private OperationViewModel OperationViewModelFor(
      OperationEntry operationEntry, 
      OperationPropertiesViewModelBuilder operationPropertiesViewModelBuilder, 
      OperationStateMachine defaultOperationStateMachine) => 
        new OperationViewModel(
          operationEntry.Name,
          operationEntry.DependencyName, 
          AllowingCommandExecution(), 
          operationPropertiesViewModelBuilder, 
          defaultOperationStateMachine);

    private OperationCommandFactory AllowingCommandExecution() => 
      new OperationCommandFactory(_applicationContext);

    private static DefaultOperationStateMachine StateMachineFor(OperationEntry o) 
      => new DefaultOperationStateMachine(
            o.Operation,
            new NotExecutableOperationState(),
            new CancellationTokenSource());
  }
}