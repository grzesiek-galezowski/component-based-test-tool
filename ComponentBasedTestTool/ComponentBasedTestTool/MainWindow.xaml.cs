﻿using System.Windows;
using ComponentBasedTestTool.ViewModels;

namespace ComponentBasedTestTool
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow(
      OperationsViewModel operationsViewModel,
      OperationsOutputViewModel operationsOutputViewModel)
    {
      InitializeComponent();
      OperationsView.DataContext = operationsViewModel;
      OperationsOutputView.DataContext = operationsOutputViewModel;
    }
  }
}