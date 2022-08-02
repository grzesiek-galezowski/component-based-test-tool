namespace ViewModelsGlueCode.Interfaces;

public interface IPropertyValueSource<T>
{
  T Value { get; }
  string Name { get; }
}