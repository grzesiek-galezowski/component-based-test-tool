namespace ExtensionPoints.ImplementedByContext;

public interface IPersistable
{
  void StoreIn(IPersistentStorage persistentStorage);
}