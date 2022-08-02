namespace ViewModelsGlueCode.Interfaces;

public interface IPropertySetBuilder
{
  IPropertyValuesBuilder<T> Property<T>(string name);
  object Retrieve();
  object Object { get; }
}