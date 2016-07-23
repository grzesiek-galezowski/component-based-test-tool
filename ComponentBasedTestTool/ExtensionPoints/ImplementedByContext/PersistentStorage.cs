namespace ExtensionPoints.ImplementedByContext
{
  public interface PersistentStorage
  {
    void Store(params Persistable[] persistables);
    void StoreValue<T>(string name, T value);
  }
}