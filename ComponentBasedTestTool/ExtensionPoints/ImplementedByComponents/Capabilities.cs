namespace ExtensionPoints.ImplementedByComponents;

public static class Capabilities
{
  public interface ICustomGui
  {
    void ShowCustomUi();
  }

  public interface ICleanupOnEnvironmentClosing
  {
    void CleanupOnClosing();
  }


  public interface IAll : ICustomGui, ICleanupOnEnvironmentClosing
  {
      
  }
}