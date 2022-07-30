using ComponentSpecification.AutomationLayer;
using Xunit;
using static ComponentSpecification.ComponentAny;

namespace ComponentSpecification;

public class GivenTwoComponentInstancesAreLoaded
{
  private readonly string _componentName1 = Any.ComponentName();
  private readonly string _componentName2 = Any.ComponentName();
  private readonly string _operationName11 = Any.OperationName();
  private readonly string _parameterName1 = Any.ParameterName();
  private readonly string _parameterValue1 = Any.ParameterValue();
  private readonly string _parameterName2 = Any.ParameterName();
  private readonly string _parameterValue2 = Any.ParameterValue();
  private readonly string _operationName21 = Any.OperationName();
  private readonly string _parameterName3 = Any.ParameterName();
  private readonly string _parameterName4 = Any.ParameterName();
  private readonly string _parameterValue3 = Any.ParameterValue();
  private readonly string _parameterValue4 = Any.ParameterValue();

  [Fact]
  public void ShouldDisplayOperationsOnScriptViewInOrderOfAddition()
  {
    var context = new ComponentBasedTestToolDriver();
    context.ComponentsSetup.Add(_componentName1)
      .WithOperation(_operationName11)
      .WithParameter(_parameterName1, _parameterValue1)
      .WithParameter(_parameterName2, _parameterValue2);
    context.ComponentsSetup.Add(_componentName2)
      .WithOperation(_operationName21)
      .WithParameter(_parameterName3, _parameterValue3)
      .WithParameter(_parameterName4, _parameterValue4);

    context.StartApplication();

    context.ComponentsView.AddInstanceOf(_componentName1);
    context.ComponentsView.AddInstanceOf(_componentName2);

    context.AddToScriptView(_componentName1, _operationName11);
    context.AddToScriptView(_componentName1, _operationName11);
    context.AddToScriptView(_componentName2, _operationName21);
    context.ScriptView.AssertShowsExactly(_operationName11, _operationName11, _operationName21);
  }

  [Fact]
  public void ShouldDisplayOperationPropertiesOnScriptView()
  {
    var driver = new ComponentBasedTestToolDriver();
    driver.ComponentsSetup.Add(_componentName1)
      .WithOperation(_operationName11)
      .WithParameter(_parameterName1, _parameterValue1)
      .WithParameter(_parameterName2, _parameterValue2);
    driver.ComponentsSetup.Add(_componentName2)
      .WithOperation(_operationName21)
      .WithParameter(_parameterName3, _parameterValue3)
      .WithParameter(_parameterName4, _parameterValue4);

    driver.StartApplication();

    driver.ComponentsView.AddInstanceOf(_componentName1);
    driver.ComponentsView.AddInstanceOf(_componentName2);

    driver.AddToScriptView(_componentName1, _operationName11);
    driver.ScriptView.Select(_componentName1, _operationName11);
    driver.PropertiesView.AssertShowsExactly(
      Property(_parameterName1, _parameterValue1),
      Property(_parameterName2, _parameterValue2));
  }

  [Fact]
  public void ShouldKeepDisplayingPropertiesOfLastSelectedOperationEvenAfterAnotherComponentIsSelected()
  {
    var context = new ComponentBasedTestToolDriver();
    context.ComponentsSetup.Add(_componentName1)
      .WithOperation(_operationName11)
      .WithParameter(_parameterName1, _parameterValue1)
      .WithParameter(_parameterName2, _parameterValue2);
    context.ComponentsSetup.Add(_componentName2)
      .WithOperation(_operationName21)
      .WithParameter(_parameterName3, _parameterValue3)
      .WithParameter(_parameterName4, _parameterValue4);

    context.StartApplication();

    context.ComponentsView.AddInstanceOf(_componentName1);
    context.ComponentsView.AddInstanceOf(_componentName2);

    context.InstancesView.Select(_componentName1);
    context.OperationsView.Select(_operationName11);
    context.AddToScriptView(_componentName1, _operationName11);
    context.InstancesView.Select(_componentName2);
    context.PropertiesView.AssertShowsExactly(
      Property(_parameterName1, _parameterValue1),
      Property(_parameterName2, _parameterValue2));
  }
}