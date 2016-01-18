using System;
using ExtensionPoints;
using ExtensionPoints.ImplementedByContext;
using ViewModels.GlueCode;

namespace ViewModels.ViewModels
{
  public class OperationPropertiesViewModelBuilder : OperationParametersListBuilder
  {
    readonly PropertySetBuilder _propertySetBuilder;
    private object _cachedObject = null;

    public OperationPropertiesViewModelBuilder(string name)
    {
      var scope = new PropertyObjectBuilderScope();
      _propertySetBuilder = scope.NewPropertySet(name);
    }

    public OperationParameter<string> Path(string name, string defaultValue)
    {
      var prop = _propertySetBuilder
        .Property<string>(name)
        .InitialValue(defaultValue)
        .End();
      return new RunSharpBasedParameter<string>(prop);
    }

    public OperationParameter<bool> Flag(string name, bool defaultValue)
    {
      var prop = _propertySetBuilder
        .Property<bool>(name)
        .InitialValue(defaultValue)
        .End();
      return new RunSharpBasedParameter<bool>(prop);
    }

    public OperationParameter<TimeSpan> Seconds(string name, int amount)
    {
      var prop = _propertySetBuilder
        .Property<int>(name)
        .InitialValue(amount)
        .End();
      return new RunSharpBasedSecondsParameter(prop);
    }

    public object Build()
    {
      return _propertySetBuilder.Build();
    }
  }
}