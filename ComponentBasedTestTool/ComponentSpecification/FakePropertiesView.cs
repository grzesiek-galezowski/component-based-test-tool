using System;
using NUnit.Framework;
using ViewModels.ViewModels;

namespace ComponentSpecification
{
  public class FakePropertiesView
  {
    private readonly OperationPropertiesViewModel _operationPropertiesViewModel;

    public FakePropertiesView(OperationPropertiesViewModel operationPropertiesViewModel)
    {
      _operationPropertiesViewModel = operationPropertiesViewModel;
    }

    public void AssertShowsExactly(params Tuple<string, string>[] expectedProperties)
    {
      var propertiesSetOnViewModel = _operationPropertiesViewModel.Properties;
      Assert.NotNull(propertiesSetOnViewModel);
      var existingPropertiesType = propertiesSetOnViewModel.GetType();
      foreach (var expectedProperty in expectedProperties)
      {
        var propertyInfo = existingPropertiesType.GetProperty(expectedProperty.Item1);
        Assert.NotNull(propertyInfo);
        Assert.AreEqual(expectedProperty.Item2, propertyInfo.GetValue(propertiesSetOnViewModel));
      }
    }
  }
}