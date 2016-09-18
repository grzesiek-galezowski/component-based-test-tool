using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Domain.OperationStates;
using ComponentBasedTestTool.Views.Ports;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ExtensionPoints.ImplementedByContext.StateMachine;
using ViewModels.Composition;
using ViewModels.ViewModels;
using ViewModelsGlueCode.Interfaces;

namespace ComponentBasedTestTool
{
  public class WpfOperationViewModelFactory : OperationViewModelFactory
  {
    private readonly ApplicationContext _applicationContext;
    private readonly ScriptOperationsViewModel _scriptOperationsViewModel;
    private readonly PropertySetBuilderFactory _propertySetBuilderFactory;

    public WpfOperationViewModelFactory(
      ApplicationContext applicationContext, 
      ScriptOperationsViewModel scriptOperationsViewModel, 
      PropertySetBuilderFactory propertySetBuilderFactory)
    {
      _applicationContext = applicationContext;
      _scriptOperationsViewModel = scriptOperationsViewModel;
      _propertySetBuilderFactory = propertySetBuilderFactory;
    }

    public OperationViewModel CreateOperationViewModel(OperationEntry operationEntry, OperationStateMachine operationStateMachine)
    {
      //bug move it elsewhere
      var propertySetBuilder = _propertySetBuilderFactory.CreateNewPropertySet(operationEntry.Name);
      var operationPropertiesViewModelBuilder = 
        new OperationPropertiesViewModelBuilder(propertySetBuilder);

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