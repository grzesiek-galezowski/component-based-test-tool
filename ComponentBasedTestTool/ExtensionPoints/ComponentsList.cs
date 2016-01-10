namespace ExtensionPoints
{
  public interface ComponentsList
  {
    void Add(string name, TestComponentInstanceFactory factory);
  }
}