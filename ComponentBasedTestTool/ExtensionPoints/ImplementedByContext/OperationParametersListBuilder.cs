using System;

namespace ExtensionPoints.ImplementedByContext
{
  public interface OperationParametersListBuilder
  {
    OperationParameter<string> Path(string name, string defaultValue);
    OperationParameter<bool> Flag(string name, bool defaultValue);
    OperationParameter<TimeSpan> Seconds(string name, int amount);
    OperationParameter<string> Text(string name, string defaultValue);
  }

  public interface OperationParameter<out T> : Persistable
  {
    T Value { get; }
  }

}