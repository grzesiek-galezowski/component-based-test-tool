﻿using System.Windows.Controls;
using ViewModels.ViewModels;

namespace ComponentBasedTestTool.Views.Views
{
  /// <summary>
  /// Interaction logic for ComponentsView.xaml
  /// </summary>
  public partial class ComponentsView : UserControl
  {
    public ComponentsView()
    {
      InitializeComponent();
    }

    public void SetComponentInstancesViewDataContext(ComponentInstancesViewModel componentInstancesViewModel)
    {
      ComponentInstancesView.DataContext = componentInstancesViewModel;
    }
  }
}
