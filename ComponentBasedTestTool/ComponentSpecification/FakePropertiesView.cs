using System;
using System.Collections.Generic;
using System.Reflection;
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

    public void AssertShowsExactly(params KeyValuePair<string, string>[] expectedProperties)
    {
      var propertiesSetOnViewModel = _operationPropertiesViewModel.Properties;
      Assert.NotNull(propertiesSetOnViewModel);
      foreach (var expectedProperty in expectedProperties)
      {
        AssertIsSet(expectedProperty, propertiesSetOnViewModel);
      }
    }

    private static void AssertIsSet(KeyValuePair<string, string> expectedProperty, object propertiesSetOnViewModel)
    {
      var existingPropertiesType = propertiesSetOnViewModel.GetType();
      var propertyInfo = existingPropertiesType.GetProperty(expectedProperty.Key);
      Assert.NotNull(propertyInfo);
      Assert.AreEqual(expectedProperty.Value, propertyInfo.GetValue(propertiesSetOnViewModel));
    }
  }
}