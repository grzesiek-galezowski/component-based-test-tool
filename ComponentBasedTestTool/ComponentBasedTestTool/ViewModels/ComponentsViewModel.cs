using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComponentBasedTestTool.Annotations;
using ComponentBasedTestTool.ViewModels.Commands;
using Components;
using ExtensionPoints;

namespace ComponentBasedTestTool.ViewModels
{
  public class ComponentsViewModel : INotifyPropertyChanged, ComponentsList
  {
    private List<TestComponentViewModel> _testComponentViewModels;
    private readonly TestComponentViewModelFactory _testComponentViewModelFactory;

    public ComponentsViewModel(TestComponentViewModelFactory testComponentViewModelFactory)
    {
      _testComponentViewModels = new List<TestComponentViewModel>();
      _testComponentViewModelFactory = testComponentViewModelFactory;
    }

    public List<TestComponentViewModel> TestComponents
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

    public void Add(string name, TestComponentInstanceFactory instanceFactory)
    {
      TestComponents.Add(
        _testComponentViewModelFactory.Create(name, instanceFactory)
      );
    }
  }

  public class TestComponentViewModelFactory
  {
    private readonly ComponentInstancesViewModel _componentInstancesViewModel;
    private readonly OutputFactory _outputFactory;

    public TestComponentViewModelFactory(ComponentInstancesViewModel componentInstancesViewModel, OutputFactory outputFactory)
    {
      _componentInstancesViewModel = componentInstancesViewModel;
      _outputFactory = outputFactory;
    }

    public TestComponentViewModel Create(
      string name, 
      TestComponentInstanceFactory instanceFactory)
    {
      return new TestComponentViewModel(
        name, 
        _componentInstancesViewModel, 
        new ComponentInstanceViewModelFactory(
          instanceFactory, 
          _outputFactory));
    }
  }
}