using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
using ViewModels.ViewModels;

namespace ComponentBasedTestTool
{
  public class WpfOperationViewModelFactory : OperationViewModelFactory
  {
    private readonly ApplicationContext _applicationContext;
    private readonly BackgroundTasks _backgroundTasks;

    public WpfOperationViewModelFactory(ApplicationContext applicationContext, BackgroundTasks backgroundTasks)
    {
      _applicationContext = applicationContext;
      _backgroundTasks = backgroundTasks;
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
      OperationSignals defaultOperationStateMachine) => 
        new OperationViewModel(
          operationEntry.Name,
          operationEntry.DependencyName, 
          AllowingCommandExecution(), 
          operationPropertiesViewModelBuilder, 
          defaultOperationStateMachine);

    private OperationCommandFactory AllowingCommandExecution() => 
      new OperationCommandFactory(_applicationContext);

    private DefaultOperationStateMachine StateMachineFor(OperationEntry o)
    {
      return new DefaultOperationStateMachine(
        o.Operation,
        new UnavailableOperationState(), new OperationStatesFactory(_backgroundTasks));
    }
  }
}