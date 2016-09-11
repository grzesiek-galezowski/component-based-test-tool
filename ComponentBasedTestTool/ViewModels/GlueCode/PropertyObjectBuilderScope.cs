using System;
using TriAxis.RunSharp;

namespace ViewModels.GlueCode
{
  public class PropertyObjectBuilderScope
  {
    private readonly AssemblyGen _assembly;

    public PropertyObjectBuilderScope()
    {
      _assembly = new AssemblyGen("Whatever", new CompilerOptions());
    }

    public PropertySetBuilder NewPropertySet(string name)
    {
      return new PropertySetBuilder(
        _assembly.Public.Class(DebugName(name))
      );
    }

    private static string DebugName(string name)
    {
      return name + $" ({Guid.NewGuid().ToString("N")})";
    }
  }
}