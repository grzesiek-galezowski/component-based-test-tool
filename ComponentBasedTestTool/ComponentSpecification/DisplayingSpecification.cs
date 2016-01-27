using System;
using System.Collections.Generic;
using NUnit.Framework;
using TddEbook.TddToolkit;

namespace ComponentSpecification
{
  public class DisplayingSpecification
  {
    private const string ComponentNameSeed = "Component";
    private const string OperationNameSeed = "Operation";
    private const string ParameterNameSeed = "ParameterName";
    private const string ParameterValueSeed = "ParameterValue";

    [Test]
    public void ShouldShowNoComponentWhenNoComponentWasLoaded()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      //WHEN
      context.StartApplication();

      //THEN
      context.AssertNoComponentsAreLoaded();
    }

    [Test]
    public void ShouldShowTheNamesOfTheAddedComponents()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = Any.String(ComponentNameSeed);
      var componentName2 = Any.String(ComponentNameSeed);
      var componentName3 = Any.String(ComponentNameSeed);
      context.ComponentsSetup.AddWithNames(componentName1, componentName2, componentName3);

      //WHEN
      context.StartApplication();

      //THEN
      context.ComponentsView
        .AssertExactlyTheFollowingAreLoaded(componentName1, componentName2, componentName3);
    }

    [Test] //note one step further- maybe nested contexts?
    public void ShouldShowNameOfTheAddedInstances()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = Any.String(ComponentNameSeed);
      var componentName2 = Any.String(ComponentNameSeed);
      var componentName3 = Any.String(ComponentNameSeed);
      context.ComponentsSetup.AddWithNames(componentName1, componentName2, componentName3);

      context.StartApplication();

      //WHEN
      context.ComponentsView.AddInstanceOf(componentName1);
      context.ComponentsView.AddInstanceOf(componentName1);
      context.ComponentsView.AddInstanceOf(componentName2);

      //THEN
      context.InstancesView.AssertShowsExactlyTheFollowingInstances(componentName1, 2);
      context.InstancesView.AssertShowsExactlyTheFollowingInstances(componentName2, 1);
      context.InstancesView.AssertShowsExactlyTheFollowingInstances(componentName3, 0);
    }

    [Test]
    public void ShouldShowNamesOfOperationsOfTheInitiallySelectedComponentInstance()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = Any.String(ComponentNameSeed);
      var operationName11 = Any.String(OperationNameSeed);
      var operationName12 = Any.String(OperationNameSeed);

      context.ComponentsSetup.Add(componentName1)
        .WithOperation(operationName11)
        .WithOperation(operationName12);

      context.StartApplication();
      context.ComponentsView.AddInstanceOf(componentName1);

      //WHEN
      context.InstancesView.Select(componentName1);

      //THEN
      context.OperationsView.AssertShowsExactly(operationName11, operationName12);
    }

    [Test]
    public void ShouldShowNamesOfOperationsOfTheLastSelectedComponentInstance()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = Any.String(ComponentNameSeed);
      var operationName11 = Any.String(OperationNameSeed);
      var operationName12 = Any.String(OperationNameSeed);
      var componentName2 = Any.String(ComponentNameSeed);
      var component2Operation1 = Any.String(OperationNameSeed);
      var component2Operation2 = Any.String(OperationNameSeed);

      context.ComponentsSetup.Add(componentName1)
        .WithOperation(operationName11)
        .WithOperation(operationName12);
      context.ComponentsSetup.Add(componentName2)
        .WithOperation(component2Operation1)
        .WithOperation(component2Operation2);

      context.StartApplication();
      context.ComponentsView.AddInstanceOf(componentName1);
      context.ComponentsView.AddInstanceOf(componentName2);

      //WHEN
      context.InstancesView.Select(componentName1);
      context.InstancesView.Select(componentName2);

      //THEN
      context.OperationsView.AssertShowsExactly(component2Operation1, component2Operation2);
    }

    [Test]
    public void ShouldShowPropertiesOfInitialSelectedOperations()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = Any.String(ComponentNameSeed);
      var operationName11 = Any.String(OperationNameSeed);
      var parameterName1 = Any.String(ParameterNameSeed);
      var parameterValue1 = Any.String(ParameterValueSeed);
      var parameterName2 = Any.String(ParameterNameSeed);
      var parameterValue2 = Any.String(ParameterValueSeed);

      context.ComponentsSetup.Add(componentName1)
        .WithOperation(operationName11)
          .WithParameter(parameterName1, parameterValue1)
          .WithParameter(parameterName2, parameterValue2);

      context.StartApplication();
      context.ComponentsView.AddInstanceOf(componentName1);
      context.InstancesView.Select(componentName1);

      //WHEN
      context.OperationsView.Select(operationName11);

      //THEN
      context.PropertiesView.AssertShowsExactly(
        Property(parameterName1, parameterValue1),
        Property(parameterName2, parameterValue2)
      );
    }

    [Test]
    public void ShouldShowPropertiesOfLastSelectedOperations()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = Any.String(ComponentNameSeed);
      var operationName11 = Any.String(OperationNameSeed);
      var operationName12 = Any.String(OperationNameSeed);
      var parameterName1 = Any.String(ParameterNameSeed);
      var parameterValue1 = Any.String(ParameterValueSeed);
      var parameterName2 = Any.String(ParameterNameSeed);
      var parameterValue2 = Any.String(ParameterValueSeed);

      context.ComponentsSetup.Add(componentName1)
        .WithOperation(operationName11)
          .WithParameter(Any.String(), Any.String())
          .WithParameter(Any.String(), Any.String())
        .WithOperation(operationName12)
          .WithParameter(parameterName1, parameterValue1)
          .WithParameter(parameterName2, parameterValue2);

      context.StartApplication();
      context.ComponentsView.AddInstanceOf(componentName1);
      context.InstancesView.Select(componentName1);
      context.OperationsView.Select(operationName11);

      //WHEN
      context.OperationsView.Select(operationName12);

      //THEN
      context.PropertiesView.AssertShowsExactly(
        Property(parameterName1, parameterValue1),
        Property(parameterName2, parameterValue2)
      );
    }

    [Test]
    public void ShouldExecuteLoadedOperationWhenTriggeredFromView()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();
      var componentName1 = Any.String(ComponentNameSeed);
      var operationName11 = Any.String(OperationNameSeed);

      context.ComponentsSetup.Add(componentName1)
        .WithOperation(operationName11);

      context.StartApplication();
      context.ComponentsView.AddInstanceOf(componentName1);
      context.InstancesView.Select(componentName1);
      context.OperationsView.Select(operationName11);

      //WHEN
      context.OperationsView.ExecuteSelectedOperation();

      //THEN
      context.Operations.AssertWasRun(operationName11);
    }


    private static KeyValuePair<string, string> Property(string parameterName1, string parameterValue1)
    {
      return new KeyValuePair<string, string>(parameterName1, parameterValue1);
    }
  }
}
