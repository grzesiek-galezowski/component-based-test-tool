using System;
using System.Collections.Generic;
using System.Threading;
using ComponentSpecification.AutomationLayer;
using TddEbook.TddToolkit;
using Xunit;

// ReSharper disable ArgumentsStyleLiteral

namespace ComponentSpecification
{
  public class DisplayingSpecification
  {
    private const string ComponentNameSeed = "Component";
    private const string OperationNameSeed = "Operation";
    private const string ParameterNameSeed = "ParameterName";
    private const string ParameterValueSeed = "ParameterValue";


    [Fact]
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

    [Fact] //note one step further- maybe nested contexts?
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

    [Fact]
    public void ShouldShowNamesOfOperationsOfTheInitiallySelectedComponentInstance()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = AnyComponentName();
      var operationName11 = AnyOperationName();
      var operationName12 = AnyOperationName();

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

    [Fact]
    public void ShouldShowNamesOfOperationsOfTheLastSelectedComponentInstance()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = AnyComponentName();
      var operationName11 = AnyOperationName();
      var operationName12 = AnyOperationName();
      var componentName2 = AnyComponentName();
      var component2Operation1 = AnyOperationName();
      var component2Operation2 = AnyOperationName();

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

    [Fact]
    public void ShouldShowPropertiesOfInitialSelectedOperations()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = AnyComponentName();
      var operationName11 = AnyOperationName();
      var parameterName1 = AnyParameterName();
      var parameterValue1 = AnyParameterValue();
      var parameterName2 = AnyParameterName();
      var parameterValue2 = AnyParameterValue();

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

    [Fact]
    public void ShouldShowPropertiesOfLastSelectedOperations()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = AnyComponentName();
      var operationName11 = AnyOperationName();
      var operationName12 = AnyOperationName();
      var parameterName1 = AnyParameterName();
      var parameterValue1 = AnyParameterValue();
      var parameterName2 = AnyParameterName();
      var parameterValue2 = AnyParameterValue();

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

    private static string AnyParameterValue()
    {
      return Any.String(ParameterValueSeed);
    }

    private static string AnyParameterName()
    {
      return Any.String(ParameterNameSeed);
    }

    [Fact]
    public void ShouldExecuteLoadedOperationWhenTriggeredFromView()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();
      var componentName1 = AnyComponentName();
      var operationName11 = AnyOperationName();

      context.ComponentsSetup.Add(componentName1)
        .WithOperation(operationName11);

      context.StartApplication();
      context.ComponentsView.AddInstanceOf(componentName1);
      context.InstancesView.Select(componentName1);
      context.OperationsView.Select(operationName11);

      //WHEN
      context.OperationsView.ExecuteSelectedOperation();
      context.OperationsView.AssertSelectedOperationIsDisplayedAsInProgress();
      context.Operations.SucceedRunningOperation();
      context.OperationsView.AssertSelectedOperationIsDisplayedAsSuccessful();

      //THEN
      context.Operations.AssertWasRun(operationName11);
    }

    [Fact]
    public void ShouldDisplayStoppedOperation()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();
      var componentName1 = AnyComponentName();
      var operationName = AnyOperationName();

      context.ComponentsSetup.Add(componentName1)
        .WithOperation(operationName);

      context.StartApplication();
      context.ComponentsView.AddInstanceOf(componentName1);
      context.InstancesView.Select(componentName1);
      context.OperationsView.Select(operationName);

      //WHEN
      context.OperationsView.ExecuteSelectedOperation();
      context.Operations.StopRunningOperation();

      //THEN
      context.OperationsView.AssertSelectedOperationIsDisplayedAsStopped();
    }

    [Fact]
    public void ShouldAllowExecutingPreviouslyStoppedOperation()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();
      var componentName1 = AnyComponentName();
      var operationName = AnyOperationName();

      context.ComponentsSetup.Add(componentName1)
        .WithOperation(operationName);

      context.StartApplication();
      context.ComponentsView.AddInstanceOf(componentName1);
      context.InstancesView.Select(componentName1);
      context.OperationsView.Select(operationName);

      context.OperationsView.ExecuteSelectedOperation();
      context.Operations.StopRunningOperation();

      //WHEN
      context.OperationsView.ExecuteSelectedOperation();
      context.Operations.SucceedRunningOperation();

      //THEN
      context.Operations.AssertWasRun(operationName, times: 2);
      context.OperationsView.AssertSelectedOperationIsDisplayedAsSuccessful();
    }

    public static string AnyOperationName()
    {
      return Any.String(OperationNameSeed);
    }

    public static string AnyComponentName()
    {
      return Any.String(ComponentNameSeed);
    }


    private static KeyValuePair<string, string> Property(string parameterName1, string parameterValue1)
    {
      return new KeyValuePair<string, string>(parameterName1, parameterValue1);
    }


  }
}
