using CallMeMaybe;
using ExtensionPoints.ImplementedByComponents;

namespace ViewModels.ViewModels
{
  public class OperationEntry
  {
    public Maybe<string> DependencyName { get; }
    public string Name { get; }
    public ComponentOperation Operation { get; }

    public OperationEntry(string name, ComponentOperation operation, Maybe<string> dependencyName)
    {
      Name = name;
      Operation = operation;
      DependencyName = dependencyName;
    }

    public static OperationEntry With(string name, ComponentOperation operation, Maybe<string> dependencyName)
    {
      return new OperationEntry(name, operation, dependencyName);
    }

    public void FillParameters(OperationPropertiesViewModelBuilder operationPropertiesViewModelBuilder)
    {
      Operation.InitializeParameters(operationPropertiesViewModelBuilder);
    }
  }
}