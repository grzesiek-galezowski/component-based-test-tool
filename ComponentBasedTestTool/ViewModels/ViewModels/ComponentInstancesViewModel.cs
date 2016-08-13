using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels
{
  public class ComponentInstancesViewModel
  {
    private readonly OperationsViewModel _operationsViewModel;

    private ComponentInstanceViewModel _selectedInstance;

    public ComponentInstancesViewModel(OperationsViewModel operationsViewModel)
    {
      _operationsViewModel = operationsViewModel;
    }

    public ObservableCollection<ComponentInstanceViewModel> ComponentInstances { get; } 
      = new ObservableCollection<ComponentInstanceViewModel>();

    public ComponentInstanceViewModel SelectedInstance
    {
      get { return _selectedInstance; }
      set
      {
        _selectedInstance = value;
        _selectedInstance.AddOperationsTo(_operationsViewModel);
      }
    }

    public void Add(ComponentInstanceViewModel componentInstanceViewModel)
    {
      ComponentInstances.Add(componentInstanceViewModel);
    }

    public void SaveTo(PersistentModelFileContentBuilder persistentModelFileContentBuilder)
    {
      foreach (var componentInstanceViewModel in ComponentInstances)
      {
        componentInstanceViewModel.SaveTo(persistentModelFileContentBuilder);
      }
      persistentModelFileContentBuilder.Save();
    }
  }

}
