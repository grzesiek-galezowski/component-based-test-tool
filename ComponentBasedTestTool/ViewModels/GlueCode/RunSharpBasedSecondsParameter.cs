using System;
using ExtensionPoints;

namespace ViewModels.GlueCode
{
  public class RunSharpBasedSecondsParameter : OperationParameter<TimeSpan>
  {
    private readonly PropertyValueSource<int> _prop;

    public RunSharpBasedSecondsParameter(PropertyValueSource<int> prop)
    {
      _prop = prop;
    }

    public TimeSpan Value => TimeSpan.FromSeconds(_prop.Value);
  }
}