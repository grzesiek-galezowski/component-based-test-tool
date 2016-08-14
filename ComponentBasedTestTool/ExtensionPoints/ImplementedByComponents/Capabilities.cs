namespace ExtensionPoints.ImplementedByComponents
{
  public static class Capabilities
  {
    public interface CustomGui
    {
      void ShowCustomUi();
    }

    public interface All : CustomGui
    {
      
    }
  }
}