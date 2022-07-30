using TriAxis.RunSharp;

namespace ViewModelsGlueCode.Interfaces;

public interface PropertyValuesBuilder<T>
{
  RunSharpBasedPropertyValueSource<T> End();
  PropertyValuesBuilder<T> InitialValue(Operand initialValue);
  PropertyValuesBuilder<T> With<TAttribute>(params object[] options);
}