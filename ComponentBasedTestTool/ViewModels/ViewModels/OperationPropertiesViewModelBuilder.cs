using System;
using System.Collections.Generic;
using System.ComponentModel;
using ExtensionPoints;
using ExtensionPoints.ImplementedByContext;
using ViewModelsGlueCode;
using ViewModelsGlueCode.Interfaces;

namespace ViewModels.ViewModels
{
  public class OperationPropertiesViewModelBuilder : OperationParametersListBuilder
  {
    private const string GenericProperties = "Generic properties";
    readonly PropertySetBuilder _propertySetBuilder;

    public OperationPropertiesViewModelBuilder(PropertySetBuilder propertySetBuilder)
    {
      _propertySetBuilder = propertySetBuilder;
    }

    public OperationParameter<string> Path(string name, string defaultValue)
    {
      var prop = _propertySetBuilder
        .Property<string>(name)
        .InitialValue(defaultValue)
        .With<CategoryAttribute>(GenericProperties)
        .End();
      return new RunSharpBasedParameter<string>(prop);
    }

    public OperationParameter<string> Text(string name, string defaultValue)
    {
      var prop = _propertySetBuilder
        .Property<string>(name)
        .InitialValue(defaultValue)
        .With<CategoryAttribute>(GenericProperties)
        .End();
      return new RunSharpBasedParameter<string>(prop);
    }


    public OperationParameter<bool> Flag(string name, bool defaultValue)
    {
      var prop = _propertySetBuilder
        .Property<bool>(name)
        .InitialValue(defaultValue)
        .With<CategoryAttribute>(GenericProperties)
        .End();
      return new RunSharpBasedParameter<bool>(prop);
    }

    public OperationParameter<TimeSpan> Seconds(string name, int amount)
    {
      var prop = _propertySetBuilder
        .Property<int>(name)
        .InitialValue(amount)
        .With<CategoryAttribute>(GenericProperties)
        .End();
      return new RunSharpBasedSecondsParameter(prop);
    }


    public object RetrieveList()
    {
      return _propertySetBuilder.Retrieve();
    }

  }
}