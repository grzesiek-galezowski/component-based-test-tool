using System;
using ExtensionPoints.ImplementedByContext;
using ViewModelsGlueCode.Interfaces;

namespace ViewModelsGlueCode
{
  public class RunSharpBasedSecondsParameter : OperationParameter<TimeSpan>
  {
    private readonly PropertyValueSource<int> _prop;

    public RunSharpBasedSecondsParameter(PropertyValueSource<int> prop)
    {
      _prop = prop;
    }

    public TimeSpan Value => TimeSpan.FromSeconds(_prop.Value);
    public void StoreIn(PersistentStorage persistentStorage)
    {
      persistentStorage.StoreValue(_prop.Name, _prop.Value);
    }
  }
}