namespace ExtensionPoints.ImplementedByContext;

public interface Persistable
{
  void StoreIn(PersistentStorage persistentStorage);
}