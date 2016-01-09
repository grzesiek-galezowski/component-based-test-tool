using System.Security.Cryptography.X509Certificates;

namespace ExtensionPoints
{
  public interface TestComponent
  {
    void PopulateOperations(TestComponentContext ctx);
  }
}