using System;
using TriAxis.RunSharp;
using ViewModelsGlueCode.Interfaces;

namespace ViewModelsGlueCode
{
  public class RunSharpBasedPropertySetBuilder : CreatedPropertySetObjectContainer, PropertySetBuilder
  {
    private readonly TypeGen _typeGen;
    private object _object;

    public RunSharpBasedPropertySetBuilder(TypeGen typeGen)
    {
      _typeGen = typeGen;
    }

    public PropertyValuesBuilder<T> Property<T>(string name)
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
        return _object = Activator.CreateInstance(type);
      }
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
}