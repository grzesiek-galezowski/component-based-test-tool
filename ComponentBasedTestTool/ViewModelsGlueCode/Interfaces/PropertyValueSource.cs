namespace ViewModelsGlueCode.Interfaces
{
  public interface PropertyValueSource<T>
  {
    T Value { get; }
    string Name { get; }
  }
}