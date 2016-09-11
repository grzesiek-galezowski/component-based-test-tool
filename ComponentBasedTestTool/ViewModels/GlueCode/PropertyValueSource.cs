using TriAxis.RunSharp;

namespace ViewModels.GlueCode
{
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

    public T Value
    {
      get
      {
        var completedType = _typeGen.GetCompletedType();
        return (T)(completedType.GetProperty(Name).GetValue(_container.Object));
      }
    }

    public string Name
    {
      get { return _propertyName; }
    }
  }
}