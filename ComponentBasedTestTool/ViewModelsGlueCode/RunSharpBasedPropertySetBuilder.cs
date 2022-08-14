using System;
using Core.Maybe;
using Core.NullableReferenceTypesExtensions;
using TriAxis.RunSharp;
using ViewModelsGlueCode.Interfaces;

namespace ViewModelsGlueCode;

public class RunSharpBasedPropertySetBuilder : ICreatedPropertySetObjectContainer, IPropertySetBuilder
{
  private readonly TypeGen _typeGen;
  private Maybe<object> _object;

  public RunSharpBasedPropertySetBuilder(TypeGen typeGen)
  {
    _typeGen = typeGen;
  }

  public IPropertyValuesBuilder<T> Property<T>(string name)
  {
    return new RunSharpBasedPropertyValuesBuilder<T>(name, _typeGen, this);
  }

  public object Retrieve()
  {
    if (_typeGen.IsCompleted)
    {
      return _object;
    }
    else
    {
      _typeGen.Complete();
      var type = _typeGen.GetCompletedType();
      var instance = Activator.CreateInstance(type).OrThrow();
      _object = instance.Just();
      return instance;
    }
  }

  public object Object
  {
    get
    {
      if (_object.IsNothing())
      {
        throw new TypeNotCompletedYetException();
      }
      return _object;
    }
  }
}