using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;
using ViewModels.ViewModels.Commands;

namespace ViewModels.ViewModels
{
  public class ComponentsViewModel : INotifyPropertyChanged, ComponentsList
  {
    private ObservableCollection<TestComponentViewModel> _testComponentViewModels;
    private readonly TestComponentViewModelFactory _testComponentViewModelFactory;

    public ComponentsViewModel(TestComponentViewModelFactory testComponentViewModelFactory)
    {
      _testComponentViewModels = new ObservableCollection<TestComponentViewModel>();
      _testComponentViewModelFactory = testComponentViewModelFactory;
    }

    public ICommand AddAllSelectedCommand => new AddAllSelectedCommand();

    public ObservableCollection<TestComponentViewModel> TestComponents
    {
      get
      {
        return _testComponentViewModels;
      }
      set
      {
        _testComponentViewModels = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void Add(string name, string description, TestComponentInstanceFactory instanceFactory)
    {
      TestComponents.Add(
        _testComponentViewModelFactory.Create(name, description, instanceFactory)
      );
    }

  }
}