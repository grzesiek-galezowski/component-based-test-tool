using System;
using NUnit.Framework;
using TddEbook.TddToolkit;

namespace ComponentSpecification
{
  public class DisplayingSpecification
  {
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

      var componentName1 = Any.String();
      var componentName2 = Any.String();
      var componentName3 = Any.String();
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

      var pluginName1 = Any.String();
      var pluginName2 = Any.String();
      var pluginName3 = Any.String();
      context.ComponentsSetup.AddWithNames(pluginName1, pluginName2, pluginName3);

      context.StartApplication();

      //WHEN
      context.ComponentsView.AddInstanceOf(pluginName1);
      context.ComponentsView.AddInstanceOf(pluginName1);
      context.ComponentsView.AddInstanceOf(pluginName2);

      //THEN
      context.InstancesView.AssertShowsExactlyTheFollowingInstances(pluginName1, 2);
      context.InstancesView.AssertShowsExactlyTheFollowingInstances(pluginName2, 1);
      context.InstancesView.AssertShowsExactlyTheFollowingInstances(pluginName3, 0);
    }

    [Test]
    public void ShouldShowNamesOfOperationsOfTheInitiallySelectedComponentInstance()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = Any.String();
      var operationName11 = Any.String();
      var operationName12 = Any.String();

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

      var componentName1 = Any.String();
      var operationName11 = Any.String();
      var operationName12 = Any.String();
      var componentName2 = Any.String();
      var component2Operation1 = Any.String();
      var component2Operation2 = Any.String();

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
    public void ShouldShowNamesOfOperationsOfTheLastSelectedComponentInstance123443()
    {
      //GIVEN
      var context = new ComponentBasedTestToolDriver();

      var componentName1 = Any.String();
      var operationName11 = Any.String();

      context.ComponentsSetup.Add(componentName1)
        .WithOperation(operationName11)
          .WithParameter("param1", "value1")
          .WithParameter("param2", "value2");

      context.StartApplication();
      context.ComponentsView.AddInstanceOf(componentName1);
      context.InstancesView.Select(componentName1);

      //WHEN
      context.OperationsView.Select(operationName11);

      //THEN
      context.OperationsView.AssertShowsExactly(operationName11);
      context.PropertiesView.AssertShowsExactly(
        Tuple.Create("param1", "value1"),
        Tuple.Create("param2", "value2")
        );
    }

  }
}
