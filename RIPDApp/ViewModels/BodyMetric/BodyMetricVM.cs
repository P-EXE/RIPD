using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDShared.Models;
using System.Collections.ObjectModel;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(CreatedBodyMetric), nameof(CreatedBodyMetric))]
[QueryProperty(nameof(DeletedBodyMetric), nameof(DeletedBodyMetric))]
public partial class BodyMetricVM : ObservableObject
{
  private readonly IDiaryService _diaryService;

  public BodyMetricVM(IDiaryService diaryService)
  {
    _diaryService = diaryService;
    GetBodyMetricEntries();
  }

  [ObservableProperty]
  private ObservableCollection<DiaryEntry_BodyMetric> _bodyMetrics = [];
  [ObservableProperty]
  private DiaryEntry_BodyMetric? _selectedBodyMetric;
  [ObservableProperty]
  private DiaryEntry_BodyMetric? _createdBodyMetric;
  [ObservableProperty]
  private DiaryEntry_BodyMetric? _deletedBodyMetric;

  private async Task GetBodyMetricEntries()
  {
    IEnumerable<DiaryEntry_BodyMetric>? bodyMetrics = await _diaryService.GetBodyMetricEntriesAsync(Statics.Auth.Owner.Diary, DateTime.MinValue, DateTime.UtcNow);
    BodyMetrics = bodyMetrics!.ToObservableCollection();
  }

  partial void OnCreatedBodyMetricChanged(DiaryEntry_BodyMetric? value)
  {
    if (value == null)
    {
      Shell.Current.DisplayAlert("Error", "Unable to add Body Metric.", "Close");
      return;
    }
    BodyMetrics.Add(value);
  }

  partial void OnDeletedBodyMetricChanged(DiaryEntry_BodyMetric? value)
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
