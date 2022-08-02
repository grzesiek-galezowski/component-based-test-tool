using System;

namespace ExtensionPoints.ImplementedByContext;

public interface IOperationParametersListBuilder
{
  //todo maybe let the client use factory for parameters and then pass them to builder?
  IOperationParameter<string> Path(string name, string defaultValue);
  IOperationParameter<bool> Flag(string name, bool defaultValue);
  IOperationParameter<TimeSpan> Seconds(string name, int amount);
  IOperationParameter<string> Text(string name, string defaultValue);
}

public interface IOperationParameter<out T> : IPersistable
{
  T Value { get; }
}