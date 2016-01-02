using System;
using ComponentBasedTestTool.ViewModels;
using ExtensionPoints;

namespace ComponentBasedTestTool.GlueCode
{
  public class ViewModelBasedParameter<T> : OperationParameter<T>
  {
    private readonly OperationParameterViewModel _viewModel;
    private readonly Func<string, T> _conversion;

    public ViewModelBasedParameter(
      OperationParameterViewModel viewModel, Func<string, T> conversion)
    {
      _viewModel = viewModel;
      _conversion = conversion;
    }

    public T Value => _conversion.Invoke(_viewModel.Value);
  }
}