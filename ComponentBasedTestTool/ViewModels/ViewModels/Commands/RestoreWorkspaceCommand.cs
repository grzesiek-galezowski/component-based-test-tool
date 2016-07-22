using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Xml.Linq;

namespace ViewModels.ViewModels.Commands
{
  public class RestoreWorkspaceCommand : ICommand
  {
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;
    private readonly OperationsOutputViewModel _operationsOutputViewModel;
    private readonly RestoringOfSavedComponentsObserver _restoringOfSavedComponentsObserver;

    public RestoreWorkspaceCommand(
      ComponentInstancesViewModel componentInstancesViewModel, 
      OperationsOutputViewModel operationsOutputViewModel, 
      RestoringOfSavedComponentsObserver restoringOfSavedComponentsObserver)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _operationsOutputViewModel = operationsOutputViewModel;
      _restoringOfSavedComponentsObserver = restoringOfSavedComponentsObserver;
    }

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      var xDoc = XDocument.Load("Save.xml"); //bug redundancy
      foreach (var componentInstance in ComponentInstancesIn(xDoc))
      {
        _restoringOfSavedComponentsObserver.NotifyOnNewComponentWith(
          NameOf(componentInstance), TypeOf(componentInstance));
        ParseOperationsOf(componentInstance);
      }
    }

    private void ParseOperationsOf(XElement componentInstance)
    {
      foreach (var operation in OperationsOf(componentInstance))
      {
        _restoringOfSavedComponentsObserver.NotifyOnNextOperationWith(
          NameOf(operation), TypeOf(componentInstance));
        ParsePropertiesOf(operation);
      }
    }

    private void ParsePropertiesOf(XElement operation)
    {
      foreach (var property in ParametersOf(operation))
      {
        _restoringOfSavedComponentsObserver
          .NotifyOnPropertyWith(NameOf(property), ValueOf(property));
      }
    }

    private static IEnumerable<XElement> ParametersOf(XElement operation)
    {
      return operation.Elements("Parameter");
    }

    private static IEnumerable<XElement> ComponentInstancesIn(XDocument xDoc)
    {
      return xDoc.Root.Elements("Component");
    }

    private static IEnumerable<XElement> OperationsOf(XElement componentInstance)
    {
      return componentInstance.Elements("Operation");
    }

    private static string ValueOf(XElement property)
    {
      return property.Attribute("value").Value;
    }

    private static string NameOf(XElement property)
    {
      return property.Attribute("name").Value;
    }

    private string TypeOf(XElement componentInstance)
    {
      return componentInstance.Attribute("type").Value;
    }

    public event EventHandler CanExecuteChanged;
  }
}