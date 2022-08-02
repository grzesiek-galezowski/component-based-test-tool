using System;
using System.Collections.Generic;
using TriAxis.RunSharp;
using ViewModelsGlueCode.Interfaces;

namespace ViewModelsGlueCode;

public class RunSharpBasedPropertyValuesBuilder<T> : IPropertyValuesBuilder<T>
{
  private readonly string _propertyName;
  private readonly TypeGen _typeGen;
  private readonly ICreatedPropertySetObjectContainer _container;
  private readonly Type _type;
  private readonly string _fieldName;
  private readonly Dictionary<AttributeType, object[]> _attributes = new();

  private Operand _initialValue = Operand.FromObject(null);

  public RunSharpBasedPropertyValuesBuilder(string propertyName, TypeGen typeGen, ICreatedPropertySetObjectContainer container)
  {
    _propertyName = propertyName;
    _typeGen = typeGen;
    _container = container;
    _type = typeof(T);
    _fieldName = propertyName.ToLowerInvariant();
  }

  public RunSharpBasedPropertyValueSource<T> End()
  {
    var field = _typeGen.Private.Field(_type, _fieldName, Operand.FromObject(_initialValue));
    var property = _typeGen.Public.SimpleProperty(field, _propertyName);

    foreach (var attribute in _attributes)
    {
      property.Attribute(attribute.Key, attribute.Value);
    }

    return new RunSharpBasedPropertyValueSource<T>(_typeGen, _propertyName, _container);
  }

  //bug the problem is in operand argument
  public IPropertyValuesBuilder<T> InitialValue(Operand initialValue)
  {
    _initialValue = initialValue;
    return this;
  }

  public IPropertyValuesBuilder<T> With<TAttribute>(params object[] options)
  {
    _attributes.Add(typeof(TAttribute), options);
    return this;
  }
}