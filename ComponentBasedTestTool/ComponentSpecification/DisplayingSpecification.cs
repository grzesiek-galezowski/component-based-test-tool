﻿using ComponentSpecification.AutomationLayer;
using Xunit;

// ReSharper disable ArgumentsStyleLiteral

namespace ComponentSpecification;

public class DisplayingSpecification
{
  [Fact]
  public void ShouldShowTheNamesOfTheAddedComponents()
  {
    //GIVEN
    var context = new ComponentBasedTestToolDriver();

    var componentName1 = Any.ComponentName();
    var componentName2 = Any.ComponentName();
    var componentName3 = Any.ComponentName();
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

    var componentName1 = Any.ComponentName();
    var componentName2 = Any.ComponentName();
    var componentName3 = Any.ComponentName();
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

    var componentName1 = Any.ComponentName();
    var operationName11 = Any.OperationName();
    var operationName12 = Any.OperationName();

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

    var componentName1 = Any.ComponentName();
    var operationName11 = Any.OperationName();
    var operationName12 = Any.OperationName();
    var componentName2 = Any.ComponentName();
    var component2Operation1 = Any.OperationName();
    var component2Operation2 = Any.OperationName();

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



}