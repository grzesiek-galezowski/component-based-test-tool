using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComponentBasedTestTool.Annotations;

namespace ViewModels.ViewModels
{
  public class OperationPropertiesViewModel : INotifyPropertyChanged
  {
    private object _properties = null;

    public object Properties
    {
      get { return _properties; }
      set
      {
        _properties = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    //bug remove
    /*
    private static object GenerateProperties()
    {
      var scope = new PropertyObjectBuilderScope();
      var propertySetBuilder = scope.NewPropertySet();
      propertySetBuilder
        .Property<int>("Lolek")
        .With<CategoryAttribute>("Options")
        .InitialValue(0)
        .End()
        .Property<string>("Zenek")
        .With<CategoryAttribute>("Options")
        .InitialValue("Jadowity")
        .End();
      return propertySetBuilder.Build();
    }*/

    public void ClearPropertiesList()
    {
      this.Properties = null;
    }
  }
}