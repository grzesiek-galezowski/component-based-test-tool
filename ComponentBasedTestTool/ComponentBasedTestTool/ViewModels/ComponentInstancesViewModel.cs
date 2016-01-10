using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComponentBasedTestTool.Annotations;

namespace ComponentBasedTestTool.ViewModels
{
  public class ComponentInstancesViewModel
  {
    private readonly OperationsViewModel _operationsViewModel;

    private readonly ObservableCollection<ComponentInstanceViewModel> 
      _componentInstances = new ObservableCollection<ComponentInstanceViewModel>();

    private ComponentInstanceViewModel _selectedInstance;

    public ComponentInstancesViewModel(OperationsViewModel operationsViewModel)
    {
      _operationsViewModel = operationsViewModel;
    }

    public ObservableCollection<ComponentInstanceViewModel> ComponentInstances => _componentInstances;

    public ComponentInstanceViewModel SelectedInstance
    {
      get { return _selectedInstance; }
      set
      {
        _selectedInstance = value;
        _selectedInstance.AddOperationsTo(_operationsViewModel);

      }
    }
  }
}
