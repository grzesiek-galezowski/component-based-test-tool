using System;
using ExtensionPoints.ImplementedByContext;
using ViewModelsGlueCode.Interfaces;

namespace ViewModelsGlueCode;

public class RunSharpBasedSecondsParameter : IOperationParameter<TimeSpan>
{
  private readonly IPropertyValueSource<int> _prop;

  public RunSharpBasedSecondsParameter(IPropertyValueSource<int> prop)
  {
    _prop = prop;
  }

  public TimeSpan Value => TimeSpan.FromSeconds(_prop.Value);
  public void StoreIn(IPersistentStorage persistentStorage)
  {
    persistentStorage.StoreValue(_prop.Name, _prop.Value);
  }
}