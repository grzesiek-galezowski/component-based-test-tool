using System.Collections.Generic;
using System.Linq;
using CallMeMaybe;
using ExtensionPoints;
using ExtensionPoints.ImplementedByComponents;

namespace ViewModels.ViewModels
{
  public class OperationEntries
  {
    private readonly List<OperationEntry> _operations;

    public OperationEntries()
    {
      _operations = new List<OperationEntry>();
    }

    public void Add(string name, Operation operation, Maybe<string> dependencyName)
    {
      _operations.Add(OperationEntry.With(name, operation, dependencyName));
    }

    public OperationViewModels ConvertUsing(OperationViewModelFactory operationViewModelFactory)
    {
      var operationViewModels = OperationViewModels
        .CreateOperationViewModels(operationViewModelFactory, _operations);

      return operationViewModels;
    }
  }

  public class OperationEntry
  {
    public Maybe<string> DependencyName { get; }
    public string Name { get; }
    public Operation Operation { get; }

    public OperationEntry(string name, Operation operation, Maybe<string> dependencyName)
    {
      Name = name;
      Operation = operation;
      DependencyName = dependencyName;
    }

    public static OperationEntry With(string name, Operation operation, Maybe<string> dependencyName)
    {
      return new OperationEntry(name, operation, dependencyName);
    }

    public void FillParameters(OperationPropertiesViewModelBuilder operationPropertiesViewModelBuilder)
    {
      Operation.FillParameters(operationPropertiesViewModelBuilder);
    }
  }
}