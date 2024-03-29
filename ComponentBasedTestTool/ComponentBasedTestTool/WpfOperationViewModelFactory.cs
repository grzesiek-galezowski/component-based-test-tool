using ComponentBasedTestTool.Domain;
using ComponentBasedTestTool.Views.Ports;
using ViewModels.Composition;
using ViewModels.ViewModels;
using ViewModelsGlueCode.Interfaces;

namespace ComponentBasedTestTool;

public class WpfOperationViewModelFactory : IOperationViewModelFactory
{
  private readonly IApplicationContext _applicationContext;
  private readonly ScriptOperationsViewModel _scriptOperationsViewModel;
  private readonly PropertySetBuilderFactory _propertySetBuilderFactory;

  public WpfOperationViewModelFactory(
    IApplicationContext applicationContext, 
    ScriptOperationsViewModel scriptOperationsViewModel, 
    PropertySetBuilderFactory propertySetBuilderFactory)
  {
    _applicationContext = applicationContext;
    _scriptOperationsViewModel = scriptOperationsViewModel;
    _propertySetBuilderFactory = propertySetBuilderFactory;
  }

  public OperationViewModel CreateOperationViewModel(OperationEntry operationEntry, IOperationStateMachine operationStateMachine)
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
    IOperationStateMachine defaultOperationStateMachine)
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

  private OperationCommandFactory AllowingCommandExecution() => new(_applicationContext, _scriptOperationsViewModel);

}