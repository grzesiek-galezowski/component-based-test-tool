namespace ViewModelsGlueCode.Interfaces
{
  public class PropertySetBuilderFactory //bug to interface
  {
    public RunSharpBasedPropertySetBuilder CreateNewPropertySet(string name)
    {
      var propertyObjectBuilderScope = new PropertyObjectBuilderScope();
      var propertySetBuilder = propertyObjectBuilderScope.NewPropertySet(name);
      return propertySetBuilder;
    }
  }
}