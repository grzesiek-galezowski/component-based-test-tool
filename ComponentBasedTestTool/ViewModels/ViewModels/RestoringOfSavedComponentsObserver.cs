namespace ViewModels.ViewModels;

public class RestoringOfSavedComponentsObserver
{
  private readonly OperationsOutputViewModel _operationsOutputViewModel;

  public RestoringOfSavedComponentsObserver(OperationsOutputViewModel operationsOutputViewModel)
  {
    _operationsOutputViewModel = operationsOutputViewModel;
  }

  public void NotifyOnNewComponentWith(string name, string type)
  {
    _operationsOutputViewModel.WriteLine(name + ", " + type);
  }

  public void NotifyOnNextOperationWith(string name, string type)
  {
    _operationsOutputViewModel.WriteLine(" " + name + ", " + type);
  }

  public void NotifyOnPropertyWith(string name, string value)
  {
    _operationsOutputViewModel.WriteLine("  " + name + " --> " + value);
  }
}