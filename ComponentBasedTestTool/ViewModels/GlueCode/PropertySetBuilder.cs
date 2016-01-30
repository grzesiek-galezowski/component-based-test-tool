using System;
using TriAxis.RunSharp;

namespace ViewModels.GlueCode
{
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