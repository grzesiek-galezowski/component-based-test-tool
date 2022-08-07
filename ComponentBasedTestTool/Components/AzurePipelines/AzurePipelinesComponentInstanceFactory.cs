using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentBasedTestTool.Annotations;
using Components.FileSystem;
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
