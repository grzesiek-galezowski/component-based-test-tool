using System.Collections.Generic;
using ComponentSpecification.AutomationLayer;
using Xbehave;
using static ComponentSpecification.ComponentAny;

namespace ComponentSpecification
{
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
    public void ShouldDisplayOperationsOnScriptViewInOrderOfAddition()
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

    [Scenario]
    public void ShouldDisplayOperationPropertiesOnScriptView()
    {
      "When I add operation from the first component instance to script view"
        .x(() => _context.AddToScriptView(_componentName1, _operationName11));
      "And I select the operation on script view"
        .x(() => _context.ScriptView.Select(_componentName1, _operationName11));
      "Then the properties view should display properties of this operation"
        .x(() => _context.PropertiesView.AssertShowsExactly(
          Property(_parameterName1, _parameterValue1),
          Property(_parameterName2, _parameterValue2)));
    }


    [Scenario]
    public void ShouldKeepDisplayingPropertiesOfLastSelectedOperationEvenAfterAnotherComponentIsSelected()
    {
      $"And I selected instance {_componentName1}"
        .x(() => _context.InstancesView.Select(_componentName1));
      $"And I selected its operation {_operationName11}"
        .x(() => _context.OperationsView.Select(_operationName11));
      $"And I added the operation {_operationName11} to script view"
        .x(() => _context.AddToScriptView(_componentName1, _operationName11));
      $"When I selected instance {_componentName2}"
        .x(() => _context.InstancesView.Select(_componentName2));
      $"Then the properties view should still show properties of {_operationName11}"
        .x(() => _context.PropertiesView.AssertShowsExactly(
          Property(_parameterName1, _parameterValue1), 
          Property(_parameterName2, _parameterValue2)));
    }
  }
}