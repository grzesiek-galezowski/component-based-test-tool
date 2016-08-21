using System;
using System.Diagnostics.CodeAnalysis;
using ComponentSpecification.AutomationLayer;
using Xbehave;
using Xunit;
using static ComponentSpecification.ComponentAny;

namespace ComponentSpecification
{
  [SuppressMessage("ReSharper", "ArgumentsStyleLiteral")]
  public class GivenComponentWithSeveralOperationsIsLoaded
  {
    private readonly ComponentBasedTestToolDriver _context = new ComponentBasedTestToolDriver();
    private readonly string _componentName1 = AnyComponentName();
    private readonly string _operationName11 = AnyOperationName();
    private readonly string _parameterName1 = AnyParameterName();
    private readonly string _parameterValue1 = AnyParameterValue();
    private readonly string _parameterName2 = AnyParameterName();
    private readonly string _parameterValue2 = AnyParameterValue();
    private readonly string _operationName12 = AnyComponentName();
    private readonly string _parameterName3 = AnyParameterName();
    private readonly string _parameterName4 = AnyParameterName();
    private readonly string _parameterValue3 = AnyParameterValue();
    private readonly string _parameterValue4 = AnyParameterValue();

    [Background]
    public void Bg()
    {
      "Given a component with several operations is loaded".x(() =>
      {
        _context.ComponentsSetup.Add(_componentName1)
          .WithOperation(_operationName11)
            .WithParameter(_parameterName1, _parameterValue1)
            .WithParameter(_parameterName2, _parameterValue2)
          .WithOperation(_operationName12)
            .WithParameter(_parameterName3, _parameterValue3)
            .WithParameter(_parameterName4, _parameterValue4);

        _context.StartApplication();
      });

      "And I added an instance of this component"
        .x(() => _context.ComponentsView.AddInstanceOf(_componentName1));

    }

    [Scenario]
    public void ShouldShowPropertiesOfInitialSelectedOperations()
    {
      "And I selected the instance"
        .x(() => _context.InstancesView.Select(_componentName1));

      "When I select an operation of the instance"
        .x(() => _context.OperationsView.Select(_operationName11));

      "Then all the parameters of the operation should be shown with their values"
        .x(() => _context.PropertiesView.AssertShowsExactly(
            Property(_parameterName1, _parameterValue1),
            Property(_parameterName2, _parameterValue2)
        ));
    }

    [Scenario]
    public void ShouldAllowExecutingPreviouslyStoppedOperation()
    {
      "And I run an operation of the instance"
        .x(() => _context.StartOperation(_componentName1, _operationName11));

      "And I stop the operation"
        .x(() => _context.Operations.MakeRunningOperationStop());

      "And I run the operation again"
        .x(() => _context.StartOperation(_componentName1, _operationName11));

      "And it ends with success"
        .x(() => _context.Operations.MakeRunningOperationSucceed());

      "Then the operation should have been invoked twice".
        x(() => _context.Operations.AssertWasRun(_operationName11, times: 2));

      "And the operation should be displayed as successful"
        .x(() => _context.OperationsView.AssertSelectedOperationIsDisplayedAsSuccessful());
    }


    [Scenario]
    public void ShouldPassCustomUiCallToPlugin()
    {
      "When I ask the instance to display custom GUI"
        .x(() => _context.InstancesView.ShowCustomGui(_componentName1));

      "Then the command should be passed to the plugin"
        .x(() => _context.ComponentInstances.AssertCommandToShowCustomUiWasReceivedBy(_componentName1));
    } //TODO what about notification of state to the custom UI?

    [Scenario]
    public void ShouldPassClosingNotificationToPlugin()
    {
      "When I close the application"
        .x(() => _context.Close());

      "Then the command should be passed to the plugin"
        .x(() => _context.ComponentInstances.AssertClosingEventWasReceivedBy(_componentName1));
    }


  }


  public class GivenTwoComponentInstancesAreLoaded
  {
    private readonly ComponentBasedTestToolDriver _context = new ComponentBasedTestToolDriver();
    private readonly string _componentName1 = AnyComponentName();
    private readonly string _componentName2 = AnyComponentName();
    private readonly string _operationName11 = AnyOperationName();
    private readonly string _parameterName1 = AnyParameterName();
    private readonly string _parameterValue1 = AnyParameterValue();
    private readonly string _parameterName2 = AnyParameterName();
    private readonly string _parameterValue2 = AnyParameterValue();
    private readonly string _operationName21 = AnyOperationName();
    private readonly string _parameterName3 = AnyParameterName();
    private readonly string _parameterName4 = AnyParameterName();
    private readonly string _parameterValue3 = AnyParameterValue();
    private readonly string _parameterValue4 = AnyParameterValue();

    [Background]
    public void Bg()
    {
      "Given two components are loaded".x(() =>
      {
        _context.ComponentsSetup.Add(_componentName1)
          .WithOperation(_operationName11)
            .WithParameter(_parameterName1, _parameterValue1)
            .WithParameter(_parameterName2, _parameterValue2);
        _context.ComponentsSetup.Add(_componentName2)
          .WithOperation(_operationName21)
            .WithParameter(_parameterName3, _parameterValue3)
            .WithParameter(_parameterName4, _parameterValue4);

        _context.StartApplication();
      });

      "And I added one instance of each component"
        .x(() =>
        {
          _context.ComponentsView.AddInstanceOf(_componentName1);
          _context.ComponentsView.AddInstanceOf(_componentName2);
        });

    }

    [Scenario]
    public void ShouldXXXXXXXXXXXXX() //TODO make better name
    {
      "When I add operation from the first component instance to script view"
        .x(() => _context.AddToScriptView(_componentName1, _operationName11));
      "And I add this operation again"
        .x(() => _context.AddToScriptView(_componentName1, _operationName11));
      "And I add an operation from the second component instance to script view"
        .x(() => _context.AddToScriptView(_componentName2, _operationName21));
      "Then the script view should display the three operations in order"
        .x(() => _context.ScriptView.AssertShowsExactly(_operationName11, _operationName11, _operationName21));
    }
  }
}