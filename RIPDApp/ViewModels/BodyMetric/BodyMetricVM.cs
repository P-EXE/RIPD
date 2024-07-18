using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDShared.Models;
using System.Collections.ObjectModel;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(CreatedBodyMetric), nameof(CreatedBodyMetric))]
[QueryProperty(nameof(DeletedBodyMetric), nameof(DeletedBodyMetric))]
public partial class BodyMetricVM : ObservableObject
{
  [ObservableProperty]
  private ObservableCollection<BodyMetric> _bodyMetrics = [];
  [ObservableProperty]
  private BodyMetric? _selectedBodyMetric;
  [ObservableProperty]
  private BodyMetric? _createdBodyMetric;
  [ObservableProperty]
  private BodyMetric? _deletedBodyMetric;

  partial void OnCreatedBodyMetricChanged(BodyMetric? value)
  {
    if (value == null)
    {
      Shell.Current.DisplayAlert("Error", "Unable to add Body Metric.", "Close");
      return;
    }
    BodyMetrics.Add(value);
  }

  partial void OnDeletedBodyMetricChanged(BodyMetric? value)
  {
    if (value == null)
    {
      Shell.Current.DisplayAlert("Error", "Unable to remove Body Metric.", "Close");
      return;
    }
    BodyMetrics.Remove(value);
  }


  [RelayCommand]
  private async Task NavToCreatePage()
  {
    await Shell.Current.GoToAsync(Routes.BodyMetricCreatePage, false);
  }
  [RelayCommand]
  private async Task NavToViewPage()
  {
    await Shell.Current.GoToAsync(Routes.BodyMetricViewPage, false, new()
    {
      { "BodyMetric", SelectedBodyMetric }
    });
    SelectedBodyMetric = null;
  }
}
