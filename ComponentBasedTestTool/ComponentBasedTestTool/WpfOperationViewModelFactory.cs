using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.ViewModels.Ports;
using ComponentBasedTestTool.Views.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
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

    public OperationViewModel CreateOperationViewModel(OperationEntry operationEntry, OperationStateMachine operationStateMachine)
    {
      var operationPropertiesViewModelBuilder = 
        new OperationPropertiesViewModelBuilder(operationEntry.Name);

      operationEntry.FillParameters(operationPropertiesViewModelBuilder);
      operationPropertiesViewModelBuilder.RetrieveList();

      var operationViewModel = OperationViewModelFor(
        operationEntry, 
        operationPropertiesViewModelBuilder, operationEntry.OperationStateMachine);
      return operationViewModel;
    }

    private OperationViewModel OperationViewModelFor(
      OperationEntry operationEntry, 
      OperationPropertiesViewModelBuilder operationPropertiesViewModelBuilder, 
      OperationStateMachine defaultOperationStateMachine)
    {
      var operationViewModelFor = new OperationViewModel(
        operationEntry.Name,
        operationEntry.DependencyName,
        AllowingCommandExecution(),
        operationPropertiesViewModelBuilder,
        defaultOperationStateMachine);
      return operationViewModelFor;
    }

    private OperationCommandFactory AllowingCommandExecution() => 
      new OperationCommandFactory(_applicationContext);

  }
}