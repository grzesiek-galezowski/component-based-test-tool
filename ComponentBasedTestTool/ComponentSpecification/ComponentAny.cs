using System.Collections.Generic;
using TddEbook.TddToolkit;

namespace ComponentSpecification
{
  public class ComponentAny
  {
    public const string ComponentNameSeed = "Component";
    private const string OperationNameSeed = "Operation";
    private const string ParameterNameSeed = "ParameterName";
    private const string ParameterValueSeed = "ParameterValue";

    public static string AnyParameterValue()
    {
      return Any.String(ParameterValueSeed);
    }

    public static string AnyParameterName()
    {
      return Any.String(ParameterNameSeed);
    }

    public static string AnyOperationName()
    {
      return Any.String(OperationNameSeed);
    }

    public static string AnyComponentName()
    {
      return Any.String(ComponentNameSeed);
    }

    public static KeyValuePair<string, string> Property(string parameterName1, string parameterValue1)
    {
      return new KeyValuePair<string, string>(parameterName1, parameterValue1);
    }
  }
}