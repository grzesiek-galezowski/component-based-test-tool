using ComponentBasedTestTool.ViewModels;
using ExtensionPoints;

namespace ComponentBasedTestTool.GlueCode
{
  public class ViewModelBasedPathParameter
  {
    public static OperationParameter<string> Containing(OperationParameterViewModel viewModel)
    {
      return new ViewModelBasedParameter<string>(viewModel, s => s);
    }
  }
}