using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ComponentBasedTestTool.Views.Views;

public partial class OperationsOutputView : UserControl
{
  // keep track of what we've already seen in the VM's Content property
  private INotifyPropertyChanged? _notifier;
  private string _lastSeenContent = string.Empty;

  public OperationsOutputView()
  {
    InitializeComponent();
    DataContextChanged += OnDataContextChanged;
  }

  // public helper so other code can append messages directly
  public void AppendLog(string text)
  {
    if (string.IsNullOrEmpty(text)) return;

    // keep last-seen in sync if something else calls AppendLog directly
    _lastSeenContent += text;
    AppendTextToBox(text, clearExisting: false);
  }

  private void OnDataContextChanged(object? sender, DependencyPropertyChangedEventArgs e)
  {
    if (_notifier != null)
      _notifier.PropertyChanged -= Vm_PropertyChanged;

    _notifier = e.NewValue as INotifyPropertyChanged;

    if (_notifier != null)
      _notifier.PropertyChanged += Vm_PropertyChanged;

    // initialize log with existing Content (if the VM already has content)
    if (e.NewValue != null)
    {
      var content = GetContentValue(e.NewValue) ?? string.Empty;
      _lastSeenContent = content;
      AppendTextToBox(content, clearExisting: true);
    }
  }

  private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    if (sender == null || e?.PropertyName != "Content") return;

    var newContent = GetContentValue(sender) ?? string.Empty;

    string delta;
    // if newContent extends previous content, append only the new part
    if (newContent.Length >= _lastSeenContent.Length &&
        newContent.StartsWith(_lastSeenContent, StringComparison.Ordinal))
    {
      delta = newContent.Substring(_lastSeenContent.Length);
    }
    else
    {
      // Content was replaced — show whole thing (or you can choose to clear/merge)
      delta = newContent;
      _lastSeenContent = string.Empty; // will be set below
      AppendTextToBox(string.Empty, clearExisting: true); // clear existing document
    }

    _lastSeenContent = newContent;

    if (string.IsNullOrEmpty(delta)) return;

    AppendTextToBox(delta, clearExisting: false);
  }

  private void AppendTextToBox(string text, bool clearExisting)
  {
    LogTextBox.Dispatcher.BeginInvoke(new Action(() =>
    {
      // determine whether caret was at document end so we don't disrupt user's editing
      var caretAtEnd = LogTextBox.CaretPosition.CompareTo(LogTextBox.Document.ContentEnd) == 0;

      if (clearExisting)
        LogTextBox.Document.Blocks.Clear();

      if (!string.IsNullOrEmpty(text))
      {
        // AppendText writes into the document end and preserves existing content/formatting
        LogTextBox.AppendText(text);
      }

      // only move caret/scroll if user was already at the end
      if (caretAtEnd)
        LogTextBox.CaretPosition = LogTextBox.Document.ContentEnd;

      LogTextBox.ScrollToEnd();
    }));
  }

  private static string? GetContentValue(object source)
  {
    var prop = source.GetType().GetProperty("Content", BindingFlags.Instance | BindingFlags.Public);
    return prop?.GetValue(source) as string;
  }
}