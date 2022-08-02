using ExtensionPoints.ImplementedByContext;
using ViewModelsGlueCode.Interfaces;

namespace ViewModelsGlueCode;

public class RunSharpBasedParameter<T> : IOperationParameter<T>
{
  private readonly IPropertyValueSource<T> _prop;

  public RunSharpBasedParameter(IPropertyValueSource<T> prop)
  {
    _prop = prop;
  }

  public T Value => _prop.Value;
  public void StoreIn(IPersistentStorage persistentStorage)
  {
    persistentStorage.StoreValue(_prop.Name, Value.ToString());
  }
}