using System.Collections.ObjectModel;
using System.Windows.Input;
using ComponentBasedTestTool.Views.Ports;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels
{
  public class ComponentInstancesViewModel
  {
    private readonly ApplicationContext _applicationContext; //bug consider holding in a commands factory instead
    private readonly OperationsViewModel _operationsViewModel;

    private ComponentInstanceViewModel _selectedInstance;

    public ComponentInstancesViewModel(ApplicationContext applicationContext, OperationsViewModel operationsViewModel)
    {
      _applicationContext = applicationContext;
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

    public void SaveTo(FileBasedPersistentStorage fileBasedPersistentStorage)
    {
      foreach (var componentInstanceViewModel in ComponentInstances)
      {
        componentInstanceViewModel.SaveTo(fileBasedPersistentStorage);
      }
      fileBasedPersistentStorage.Save();
    }
  }

}
