namespace ExtensionPoints.ImplementedByComponents;

public static class Capabilities
{
  public interface CustomGui
  {
    void ShowCustomUi();
  }

  public interface CleanupOnEnvironmentClosing
  {
    void CleanupOnClosing();
  }


  public interface All : CustomGui, CleanupOnEnvironmentClosing
  {
      
  }
}