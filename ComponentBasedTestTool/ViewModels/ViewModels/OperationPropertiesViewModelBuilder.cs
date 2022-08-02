using System;
using System.ComponentModel;
using ExtensionPoints.ImplementedByContext;
using ViewModelsGlueCode;
using ViewModelsGlueCode.Interfaces;

namespace ViewModels.ViewModels;

public class OperationPropertiesViewModelBuilder : IOperationParametersListBuilder
{
  private const string GenericProperties = "Generic properties";
  readonly IPropertySetBuilder _propertySetBuilder;

  public OperationPropertiesViewModelBuilder(IPropertySetBuilder propertySetBuilder)
  {
    _propertySetBuilder = propertySetBuilder;
  }

  public IOperationParameter<string> Path(string name, string defaultValue)
  {
    var prop = _propertySetBuilder
      .Property<string>(name)
      .InitialValue(defaultValue)
      .With<CategoryAttribute>(GenericProperties)
      .End();
    return new RunSharpBasedParameter<string>(prop);
  }

  public IOperationParameter<string> Text(string name, string defaultValue)
  {
    var prop = _propertySetBuilder
      .Property<string>(name)
      .InitialValue(defaultValue)
      .With<CategoryAttribute>(GenericProperties)
      .End();
    return new RunSharpBasedParameter<string>(prop);
  }


  public IOperationParameter<bool> Flag(string name, bool defaultValue)
  {
    var prop = _propertySetBuilder
      .Property<bool>(name)
      .InitialValue(defaultValue)
      .With<CategoryAttribute>(GenericProperties)
      .End();
    return new RunSharpBasedParameter<bool>(prop);
  }

  public IOperationParameter<TimeSpan> Seconds(string name, int amount)
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