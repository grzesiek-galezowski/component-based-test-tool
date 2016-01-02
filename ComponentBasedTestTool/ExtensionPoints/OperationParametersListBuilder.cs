namespace ExtensionPoints
{
  public interface OperationParametersListBuilder
  {
    OperationParameter<string> Path(string name, string defaultValue);
    OperationParameter<bool> Flag(string name, bool defaultValue);
  }

  public interface OperationParameter<T>
  {
    T Value { get; }
  }

}