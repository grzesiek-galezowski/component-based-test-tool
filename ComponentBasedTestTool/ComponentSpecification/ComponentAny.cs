using TddXt.AnyExtensibility;

namespace ComponentSpecification;

public static class ComponentAny
{
  public const string ComponentNameSeed = "Component";
  private const string OperationNameSeed = "Operation";
  private const string ParameterNameSeed = "ParameterName";
  private const string ParameterValueSeed = "ParameterValue";

  public static string ParameterValue(this BasicGenerator basicGenerator)
  {
    return basicGenerator.String(ParameterValueSeed);
  }

  public static string ParameterName(this BasicGenerator basicGenerator)
  {
    return basicGenerator.String(ParameterNameSeed);
  }

  public static string OperationName(this BasicGenerator basicGenerator)
  {
    return basicGenerator.String(OperationNameSeed);
  }

  public static string ComponentName(this BasicGenerator basicGenerator)
  {
    return basicGenerator.String(ComponentNameSeed);
  }

  public static KeyValuePair<string, string> Property(string parameterName1, string parameterValue1)
  {
    return new KeyValuePair<string, string>(parameterName1, parameterValue1);
  }
}