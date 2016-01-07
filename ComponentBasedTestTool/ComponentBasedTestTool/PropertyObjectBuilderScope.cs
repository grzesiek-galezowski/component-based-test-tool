using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.RightsManagement;
using System.Windows;
using CallMeMaybe;
using TriAxis.RunSharp;

namespace ComponentBasedTestTool
{
  public class PropertyObjectBuilderScope
  {
    private readonly AssemblyGen _assembly;

    public PropertyObjectBuilderScope()
    {
      _assembly = new AssemblyGen("Whatever", new CompilerOptions());
    }

    public PropertySetBuilder NewPropertySet()
    {
      return new PropertySetBuilder(
        _assembly.Public.Class("PropertySet" + Guid.NewGuid().ToString("N"))
      );
    }
  }

  public class PropertySetBuilder : CreatedPropertySetObjectContainer
  {
    private readonly TypeGen _typeGen;
    private object _object;

    public PropertySetBuilder(TypeGen typeGen)
    {
      _typeGen = typeGen;
    }

    public PropertyValuesBuilder<T> Property<T>(string name)
    {
      return new PropertyValuesBuilder<T>(name, _typeGen, this);
    }

    public object Build()
    {
      _typeGen.Complete();
      var type = _typeGen.GetCompletedType();
      return _object = Activator.CreateInstance(type);
    }

    public object Object
    {
      get
      {
        if (_object == null)
        {
          throw new TypeNotCompletedYetException();
        }
        return _object;
      }
    }
  }

  public class TypeNotCompletedYetException : Exception
  {
  }

  public interface CreatedPropertySetObjectContainer
  {
    object Object { get; }
  }


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

  public class PropertyValueSource<T>
  {
    private readonly TypeGen _typeGen;
    private readonly string _propertyName;
    private readonly CreatedPropertySetObjectContainer _container;

    public PropertyValueSource(TypeGen typeGen, string propertyName, CreatedPropertySetObjectContainer container)
    {
      _typeGen = typeGen;
      _propertyName = propertyName;
      _container = container;
    }

    public T GetValue()
    {
      var completedType = _typeGen.GetCompletedType();
      return (T)(completedType.GetProperty(_propertyName).GetValue(_container.Object));
    }
  }
}