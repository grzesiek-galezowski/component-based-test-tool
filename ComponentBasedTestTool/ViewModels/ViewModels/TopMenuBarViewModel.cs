using System.Windows.Input;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels
{
  public class TopMenuBarViewModel
  {
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;
    private readonly OperationsOutputViewModel _operationsOutputViewModel;
    private readonly ComponentsViewModel _componentsViewModel;

    public TopMenuBarViewModel(
      ComponentInstancesViewModel componentInstancesViewModel, 
      OperationsOutputViewModel operationsOutputViewModel, 
      ComponentsViewModel componentsViewModel)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _operationsOutputViewModel = operationsOutputViewModel;
      _componentsViewModel = componentsViewModel;
    }

    public ICommand SaveWorkspaceCommand => new SaveWorkspaceCommand(
      _componentInstancesViewModel, 
      _operationsOutputViewModel);
    public ICommand RestoreWorkspaceCommand => new RestoreWorkspaceCommand(
      _componentInstancesViewModel, 
      _operationsOutputViewModel,
      new RestoringOfSavedComponentsObserver(_operationsOutputViewModel));
  }
}