﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints;

namespace ViewModels.ViewModels
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
}