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
}