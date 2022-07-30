using ComponentSpecification.AutomationLayer;
using Xunit;
using static ComponentSpecification.ComponentAny;

namespace ComponentSpecification;

public class GivenComponentWithSeveralOperationsIsLoaded
{
  private readonly ComponentBasedTestToolDriver _context = new();
  private readonly string _componentName1 = Any.ComponentName();
  private readonly string _operationName11 = Any.OperationName();
  private readonly string _parameterName1 = Any.ParameterName();
  private readonly string _parameterValue1 = Any.ParameterValue();
  private readonly string _parameterName2 = Any.ParameterName();
  private readonly string _parameterValue2 = Any.ParameterValue();
  private readonly string _operationName12 = Any.ComponentName();
  private readonly string _parameterName3 = Any.ParameterName();
  private readonly string _parameterName4 = Any.ParameterName();
  private readonly string _parameterValue3 = Any.ParameterValue();
  private readonly string _parameterValue4 = Any.ParameterValue();

  [Fact]
  public void ShouldShowPropertiesOfInitialSelectedOperation()
  {
    //GIVEN
    _context.ComponentsSetup.Add(_componentName1)
      .WithOperation(_operationName11)
      .WithParameter(_parameterName1, _parameterValue1)
      .WithParameter(_parameterName2, _parameterValue2)
      .WithOperation(_operationName12)
      .WithParameter(_parameterName3, _parameterValue3)
      .WithParameter(_parameterName4, _parameterValue4);

    _context.StartApplication();

    _context.ComponentsView.AddInstanceOf(_componentName1);

    //WHEN
    _context.InstancesView.Select(_componentName1);
    _context.OperationsView.Select(_operationName11);

    //THEN
    _context.PropertiesView.AssertShowsExactly(
      Property(_parameterName1, _parameterValue1),
      Property(_parameterName2, _parameterValue2)
    );
  }

  [Fact]
  public void ShouldShowPropertiesOfLastSelectedOperation()
  {
    //GIVEN
    _context.ComponentsSetup.Add(_componentName1)
      .WithOperation(_operationName11)
      .WithParameter(_parameterName1, _parameterValue1)
      .WithParameter(_parameterName2, _parameterValue2)
      .WithOperation(_operationName12)
      .WithParameter(_parameterName3, _parameterValue3)
      .WithParameter(_parameterName4, _parameterValue4);

    _context.StartApplication();

    _context.ComponentsView.AddInstanceOf(_componentName1);

    _context.InstancesView.Select(_componentName1);
    _context.OperationsView.Select(_operationName11);

    //WHEN
    _context.OperationsView.Select(_operationName12);

    //THEN
    _context.PropertiesView.AssertShowsExactly(
      Property(_parameterName3, _parameterValue3),
      Property(_parameterName4, _parameterValue4)
    );
  }

  [Fact]
  public void ShouldAllowExecutingPreviouslyStoppedOperation()
  {
    //GIVEN
    _context.ComponentsSetup.Add(_componentName1)
      .WithOperation(_operationName11)
      .WithParameter(_parameterName1, _parameterValue1)
      .WithParameter(_parameterName2, _parameterValue2)
      .WithOperation(_operationName12)
      .WithParameter(_parameterName3, _parameterValue3)
      .WithParameter(_parameterName4, _parameterValue4);

    _context.StartApplication();

    _context.ComponentsView.AddInstanceOf(_componentName1);

    //WHEN
    _context.StartOperation(_componentName1, _operationName11);
    _context.Operations.MakeRunningOperationStop();
    _context.StartOperation(_componentName1, _operationName11);
    _context.Operations.MakeRunningOperationSucceed();

    //THEN
    _context.Operations.AssertWasRun(_operationName11, times: 2);
    _context.OperationsView.AssertSelectedOperationIsDisplayedAsSuccessful();
  }


  [Fact]
  public void ShouldPassCustomUiCallToPlugin()
  {
    //GIVEN
    _context.ComponentsSetup.Add(_componentName1)
      .WithOperation(_operationName11)
      .WithParameter(_parameterName1, _parameterValue1)
      .WithParameter(_parameterName2, _parameterValue2)
      .WithOperation(_operationName12)
      .WithParameter(_parameterName3, _parameterValue3)
      .WithParameter(_parameterName4, _parameterValue4);

    _context.StartApplication();

    _context.ComponentsView.AddInstanceOf(_componentName1);

    //WHEN
    _context.InstancesView.ShowCustomGui(_componentName1);

    //THEN
    _context.ComponentInstances.AssertCommandToShowCustomUiWasReceivedBy(_componentName1);
  } //TODO what about notification of state to the custom UI?

  [Fact]
  public void ShouldPassClosingNotificationToPlugin()
  {
    //GIVEN
    _context.ComponentsSetup.Add(_componentName1)
      .WithOperation(_operationName11)
      .WithParameter(_parameterName1, _parameterValue1)
      .WithParameter(_parameterName2, _parameterValue2)
      .WithOperation(_operationName12)
      .WithParameter(_parameterName3, _parameterValue3)
      .WithParameter(_parameterName4, _parameterValue4);

    _context.StartApplication();
    _context.ComponentsView.AddInstanceOf(_componentName1);

    //WHEN
    _context.Close();

    //THEN
    _context.ComponentInstances.AssertClosingEventWasReceivedBy(_componentName1);
  }
}