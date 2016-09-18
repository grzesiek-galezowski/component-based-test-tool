namespace ViewModelsGlueCode.Interfaces
{
  public interface PropertySetBuilder
  {
    PropertyValuesBuilder<T> Property<T>(string name);
    object Retrieve();
    object Object { get; }
  }
}