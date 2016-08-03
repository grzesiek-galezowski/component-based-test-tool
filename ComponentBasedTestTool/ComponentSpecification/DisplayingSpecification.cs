using System;
using System.Collections.Generic;
using System.Threading;
using ComponentSpecification.AutomationLayer;
using TddEbook.TddToolkit;
using Xunit;
using static ComponentSpecification.ComponentAny;

// ReSharper disable ArgumentsStyleLiteral

namespace ComponentSpecification
{
  public class DisplayingSpecification
  {
    [Fact]
    public void ShouldShowTheNamesOfTheAddedComponents()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = AnyComponentName();
      var componentName2 = AnyComponentName();
      var componentName3 = AnyComponentName();
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

      var componentName1 = AnyComponentName();
      var componentName2 = AnyComponentName();
      var componentName3 = AnyComponentName();
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
      context.PropertiesView.AssertShowsExactly(Property(parameterName1, parameterValue1),
        Property(parameterName2, parameterValue2)
        );
    }


  }
}
