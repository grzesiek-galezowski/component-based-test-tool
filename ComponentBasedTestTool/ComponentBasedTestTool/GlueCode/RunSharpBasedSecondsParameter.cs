using System;
using ExtensionPoints;

namespace ComponentBasedTestTool.ViewModels
{
  public class RunSharpBasedSecondsParameter : OperationParameter<TimeSpan>
  {
    private readonly PropertyValueSource<int> _prop;

    public RunSharpBasedSecondsParameter(PropertyValueSource<int> prop)
    {
      _prop = prop;
    }

    public TimeSpan Value => TimeSpan.FromSeconds(_prop.GetValue());
  }
}