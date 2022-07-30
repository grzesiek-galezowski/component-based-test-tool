using System.Windows.Input;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels;

public class TopMenuBarViewModel
{
  private readonly ComponentInstancesViewModel _componentInstancesViewModel;
  private readonly OperationsOutputViewModel _operationsOutputViewModel;
  private readonly PersistentModelContentBuilderFactory _persistentModelContentBuilderFactory;

  public TopMenuBarViewModel(
    ComponentInstancesViewModel componentInstancesViewModel, 
    OperationsOutputViewModel operationsOutputViewModel, 
    PersistentModelContentBuilderFactory persistentModelContentBuilderFactory)
  {
    _componentInstancesViewModel = componentInstancesViewModel;
    _operationsOutputViewModel = operationsOutputViewModel;
    _persistentModelContentBuilderFactory = persistentModelContentBuilderFactory;
  }

  public ICommand SaveWorkspaceCommand => new SaveWorkspaceCommand(
    _componentInstancesViewModel, _persistentModelContentBuilderFactory);
  public ICommand RestoreWorkspaceCommand => new RestoreWorkspaceCommand(
    _componentInstancesViewModel, 
    _operationsOutputViewModel,
    new RestoringOfSavedComponentsObserver(_operationsOutputViewModel));
}