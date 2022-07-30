namespace ComponentSpecification.AutomationLayer;

public class FakeComponentInstances
{
  private readonly List<FakeComponentInstance> _fakeComponentInstances;

  public FakeComponentInstances(List<FakeComponentInstance> fakeComponentInstances)
  {
    _fakeComponentInstances = fakeComponentInstances;
  }

  public void AssertCommandToShowCustomUiWasReceivedBy(string componentName1)
  {
    var fakeComponentInstance = GetInstanceNamed(componentName1);
    fakeComponentInstance.AssertReceivedACommandToShowCustomUi();
  }

  private FakeComponentInstance GetInstanceNamed(string componentName1)
  {
    return _fakeComponentInstances.First(i => i.HasName(componentName1));
  }

  public void AssertClosingEventWasReceivedBy(string componentName1)
  {
    var fakeComponentInstance = GetInstanceNamed(componentName1);
    fakeComponentInstance.AssertReceivedClosingNotification();
  }
}