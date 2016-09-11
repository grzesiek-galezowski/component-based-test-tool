using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.Views.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
using ViewModels.Composition;
using ViewModels.ViewModels;

namespace ComponentBasedTestTool
{
  public class WpfOperationViewModelFactory : OperationViewModelFactory
  {
    private readonly ApplicationContext _applicationContext;
    private readonly ScriptOperationsViewModel _scriptOperationsViewModel;

    public WpfOperationViewModelFactory(
      ApplicationContext applicationContext, 
      ScriptOperationsViewModel scriptOperationsViewModel)
    {
      _applicationContext = applicationContext;
      _scriptOperationsViewModel = scriptOperationsViewModel;
    }

    public OperationViewModel CreateOperationViewModel(OperationEntry operationEntry, OperationStateMachine operationStateMachine)
    {
      var operationPropertiesViewModelBuilder = 
        new OperationPropertiesViewModelBuilder(operationEntry.Name);

      operationEntry.AddParametersTo(operationPropertiesViewModelBuilder);
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
        operationEntry.ParentComponentInstanceName,
        AllowingCommandExecution(),
        operationPropertiesViewModelBuilder,
        defaultOperationStateMachine);
      return operationViewModelFor;
    }

    private OperationCommandFactory AllowingCommandExecution() => 
      new OperationCommandFactory(_applicationContext, _scriptOperationsViewModel);

  }
}