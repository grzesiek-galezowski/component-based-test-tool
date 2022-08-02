using TriAxis.RunSharp;

namespace ViewModelsGlueCode.Interfaces;

public interface IPropertyValuesBuilder<T>
{
  RunSharpBasedPropertyValueSource<T> End();
  IPropertyValuesBuilder<T> InitialValue(Operand initialValue);
  IPropertyValuesBuilder<T> With<TAttribute>(params object[] options);
}