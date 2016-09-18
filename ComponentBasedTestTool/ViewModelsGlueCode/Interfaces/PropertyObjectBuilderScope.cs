using System;
using TriAxis.RunSharp;

namespace ViewModelsGlueCode.Interfaces
{
  public class PropertyObjectBuilderScope
  {
    private readonly AssemblyGen _assembly;

    public PropertyObjectBuilderScope()
    {
      _assembly = new AssemblyGen("Whatever", new CompilerOptions());
    }

    public RunSharpBasedPropertySetBuilder NewPropertySet(string name)
    {
      return new RunSharpBasedPropertySetBuilder(
        _assembly.Public.Class(DebugName(name))
      );
    }

    private static string DebugName(string name)
    {
      return name + $" ({Guid.NewGuid().ToString("N")})";
    }
  }
}