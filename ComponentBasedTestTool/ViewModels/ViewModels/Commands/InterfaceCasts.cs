namespace ViewModels.ViewModels.Commands;

public class InterfaceCasts
{
  private readonly object _testComponentInstance;

  public InterfaceCasts(object testComponentInstance)
  {
    _testComponentInstance = testComponentInstance;
  }

  public T To<T>(T nullCapabilities) where T : class
  {
    return (_testComponentInstance as T) ?? nullCapabilities;
  }
}