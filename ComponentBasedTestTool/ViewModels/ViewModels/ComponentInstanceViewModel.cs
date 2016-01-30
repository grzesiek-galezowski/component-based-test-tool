﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CallMeMaybe;
using ComponentBasedTestTool.Annotations;
using ExtensionPoints.ImplementedByComponents;
using ExtensionPoints.ImplementedByContext;

namespace ViewModels.ViewModels
{
  public class ComponentInstanceViewModel : 
    INotifyPropertyChanged, 
    TestComponentContext, 
    TestComponentOperationDestination
  {
    private string _instanceName;
    private readonly OutputFactory _outputFactory;
    private OperationViewModels _operationViewModels;
    private readonly OperationEntries _operationEntries;
    private readonly TestComponent _testComponentInstance;

    public ComponentInstanceViewModel(
      string instanceName, 
      OutputFactory outputFactory, 
      OperationEntries operationEntries, 
      TestComponent testComponentInstance)
    {
      _instanceName = instanceName;
      _outputFactory = outputFactory;
      _operationEntries = operationEntries;
      _testComponentInstance = testComponentInstance;
    }

    public void Initialize(OperationViewModelFactory operationViewModelFactory)
    {
      _testComponentInstance.CreateOperations(this);
      _testComponentInstance.PopulateOperations(this);
      _operationViewModels = _operationEntries.ConvertUsing(operationViewModelFactory);
    }

    public string InstanceName
    {
      get { return _instanceName; }
      set
      {
        _instanceName = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void AddOperationsTo(OperationsViewModel operationsViewModel)
    {
      _operationViewModels.AddTo(operationsViewModel);
    }

    public void AddOperation(string name, Operation operation, string dependencyName)
    {
      _operationEntries.Add(name, operation, Maybe.From(dependencyName));
    }

    public void AddOperation(string name, Operation operation)
    {
      _operationEntries.Add(name, operation, Maybe.Not);
    }

    public OperationsOutput CreateOutFor(string operationName)
    {
      return _outputFactory.CreateOutFor(operationName);
    }

    public void SaveTo(FileBasedPersistentStorage fileBasedPersistentStorage)
    {
      fileBasedPersistentStorage.BeginComponentInstance(this.InstanceName, _testComponentInstance);
      _testComponentInstance.PopulateOperations(fileBasedPersistentStorage);
    }
  }
}
