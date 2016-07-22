using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
using ExtensionPoints.ImplementedByComponents;
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
        operationPropertiesViewModelBuilder, DefaultOperationStateMachine.StateMachineFor(operationEntry.Operation, _backgroundTasks));
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
  }
}