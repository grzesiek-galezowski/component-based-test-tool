using ComponentBasedTestTool.ViewModels;
using ExtensionPoints;

namespace ComponentBasedTestTool.GlueCode
{
  public class ViewModelBasedFlagParameter
  {
    public static OperationParameter<bool> Containing(OperationParameterViewModel viewModel) 
      => new ViewModelBasedParameter<bool>(viewModel, bool.Parse);
  }
}