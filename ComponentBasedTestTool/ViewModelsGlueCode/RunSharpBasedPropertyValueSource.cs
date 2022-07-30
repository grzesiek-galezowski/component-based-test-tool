using TriAxis.RunSharp;
using ViewModelsGlueCode.Interfaces;

namespace ViewModelsGlueCode;

public class RunSharpBasedPropertyValueSource<T> : PropertyValueSource<T>
{
  private readonly TypeGen _typeGen;
  private readonly CreatedPropertySetObjectContainer _container;

  public RunSharpBasedPropertyValueSource(TypeGen typeGen, string propertyName, CreatedPropertySetObjectContainer container)
  {
    _typeGen = typeGen;
    Name = propertyName;
    _container = container;
  }

  public T Value
  {
    get
    {
      var completedType = _typeGen.GetCompletedType();
      return (T)(completedType.GetProperty(Name).GetValue(_container.Object));
    }
  }

  public string Name { get; }
}