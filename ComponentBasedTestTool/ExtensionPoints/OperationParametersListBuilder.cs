using System;

namespace ExtensionPoints
{
  public interface OperationParametersListBuilder
  {
    OperationParameter<string> Path(string name, string defaultValue);
    OperationParameter<bool> Flag(string name, bool defaultValue);
    OperationParameter<TimeSpan> Seconds(string name, int amount);
  }

  public interface OperationParameter<T>
  {
    T Value { get; }
  }

}