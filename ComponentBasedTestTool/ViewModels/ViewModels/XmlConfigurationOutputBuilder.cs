using System.Xml.Linq;
using ComponentBasedTestTool.Domain;
using ExtensionPoints.ImplementedByComponents;

namespace ViewModels.ViewModels;

public class XmlConfigurationOutputBuilder : IConfigurationOutputBuilder
{
  private XDocument _doc;
  private XElement _components;
  private XElement _currentOperation;
  private XElement _currentComponentInstance;

  public XmlConfigurationOutputBuilder()
  {
    InitializeXml();
  }

  public void InitializeXml()
  {
    _doc = new XDocument();
    _components = new XElement("Components");
    _doc.Add(_components);
  }

  public void AppendOperationNode(string name, IRunnable operation)
  {
    _currentOperation = new XElement("Operation",
      new XAttribute("name", name),
      new XAttribute("type", operation.GetType()));
    _currentComponentInstance.Add(_currentOperation);
  }

  public void AppendProperty<T>(string name, T value)
  {
    _currentOperation.Add(new XElement("Parameter",
      new XAttribute("name", name),
      new XAttribute("value", value)));
  }

  public void Save()
  {
    _doc.Save("Save.xml");
  }

  public void AppendComponentInstanceNode(string instanceName, ICoreTestComponent testComponentInstance)
  {
    _currentComponentInstance = new XElement("Component",
      new XAttribute("name", instanceName),
      new XAttribute("type", testComponentInstance.GetType()));
    _components.Add(_currentComponentInstance);
  }

  public override string ToString()
  {
    return _doc.ToString();
  }
}