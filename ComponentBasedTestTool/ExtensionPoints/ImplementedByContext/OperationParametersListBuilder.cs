using System;

namespace ExtensionPoints.ImplementedByContext;

public interface OperationParametersListBuilder
{
  //todo maybe let the client use factory for parameters and then pass them to builder?
  OperationParameter<string> Path(string name, string defaultValue);
  OperationParameter<bool> Flag(string name, bool defaultValue);
  OperationParameter<TimeSpan> Seconds(string name, int amount);
  OperationParameter<string> Text(string name, string defaultValue);
}

public interface OperationParameter<out T> : Persistable
{
  T Value { get; }
}