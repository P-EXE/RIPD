using RIPDShared.Models;
using System.Collections.ObjectModel;

namespace RIPDApp.Views;

public partial class BarChartV : ContentView
{
  public static readonly BindableProperty ValuesProperty = BindableProperty.Create(nameof(Values), typeof(ObservableCollection<double>), typeof(BarChartV));
  public ObservableCollection<double> Values
  {
    get => (ObservableCollection<double>)GetValue(ValuesProperty);
    set => SetValue(ValuesProperty, value);
  }
  public BarChartV()
  {
    InitializeComponent();
  }
}