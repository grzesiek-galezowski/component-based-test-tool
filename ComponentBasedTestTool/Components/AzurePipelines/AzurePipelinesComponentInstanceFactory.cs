using System.Composition;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace Components.AzurePipelines
{
  [Export(typeof(ITestComponentSourceRoot))]
  [UsedImplicitly]
  public class AzurePipelinesComponentInstanceFactory : ITestComponentInstanceFactory, ITestComponentSourceRoot
  {
    public ICoreTestComponent Create()
    {
      return new AzurePipelinesComponent();
    }

    public void AddTo(IComponentsList components)
    {
      components.Add("AzurePipelines", "Runs Azure Pipelines", this);
    }
  }
}
