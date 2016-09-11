using System;
using System.Collections.Generic;
using TriAxis.RunSharp;

namespace ViewModels.GlueCode
{
  public class PropertyValuesBuilder<T>
  {
    private readonly string _propertyName;
    private readonly TypeGen _typeGen;
    private readonly CreatedPropertySetObjectContainer _container;
    private readonly Type _type;
    private readonly string _fieldName;
    private readonly Dictionary<AttributeType, object[]> _attributes 
      = new Dictionary<AttributeType, object[]>();

    private Operand _initialValue = Operand.FromObject(null);

    public PropertyValuesBuilder(string propertyName, TypeGen typeGen, CreatedPropertySetObjectContainer container)
    {
      _propertyName = propertyName;
      _typeGen = typeGen;
      _container = container;
      _type = typeof(T);
      _fieldName = propertyName.ToLowerInvariant();
    }

    public PropertyValueSource<T> End()
    {
      var field = _typeGen.Private.Field(_type, _fieldName, Operand.FromObject(_initialValue));
      var property = _typeGen.Public.SimpleProperty(field, _propertyName);

      foreach (var attribute in _attributes)
      {
        property.Attribute(attribute.Key, attribute.Value);
      }

      return new PropertyValueSource<T>(_typeGen, _propertyName, _container);
    }

    public PropertyValuesBuilder<T> InitialValue(Operand initialValue)
    {
      _initialValue = initialValue;
      return this;
    }

    public PropertyValuesBuilder<T> With<TAttribute>(params object[] options)
    {
      _attributes.Add(typeof(TAttribute), options);
      return this;
    }
  }
}