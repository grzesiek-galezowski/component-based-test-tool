using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels
{
  public class ComponentInstancesViewModel
  {
    private readonly OperationsViewModel _operationsViewModel;
    private readonly OperationViewsViewModel _operationViewsViewModel;

    private ComponentInstanceViewModel _selectedComponentInstance;

    public ComponentInstancesViewModel(
      OperationsViewModel operationsViewModel, 
      OperationViewsViewModel operationViewsViewModel)
    {
      _operationsViewModel = operationsViewModel;
      _operationViewsViewModel = operationViewsViewModel;
    }

    public ObservableCollection<ComponentInstanceViewModel> ComponentInstances { get; } 
      = new ObservableCollection<ComponentInstanceViewModel>();

    public ComponentInstanceViewModel SelectedInstance
    {
      get { return _selectedComponentInstance; }
      set
      {
        _selectedComponentInstance = value;
        //bug on select tab, one needs to update properties view and change strategy of 
        //changing this view based on selected component
        _selectedComponentInstance.UpdateOperationsOn(_operationsViewModel);
        _operationViewsViewModel.UpdateForNewComponent(); //? good name?
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
