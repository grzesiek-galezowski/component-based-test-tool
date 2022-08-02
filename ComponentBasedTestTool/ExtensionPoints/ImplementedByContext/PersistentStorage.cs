namespace ExtensionPoints.ImplementedByContext;

public interface IPersistentStorage
{
  void Store(params IPersistable[] persistables);
  void StoreValue<T>(string name, T value);
}