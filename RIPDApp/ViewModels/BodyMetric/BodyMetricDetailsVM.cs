using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(BodyMetric), nameof(BodyMetric))]
public partial class BodyMetricDetailsVM(IDiaryService diaryService) : ObservableObject
{
  private readonly IDiaryService _diaryService = diaryService;

  [ObservableProperty]
  private DiaryEntry_BodyMetric _bodyMetric = new()
  {
    Acted = DateTime.Now
  };

  [ObservableProperty]
  private DateTime _actedDate = DateTime.UtcNow.Date;
  [ObservableProperty]
  private TimeSpan _actedTime = DateTime.UtcNow.TimeOfDay;

  [RelayCommand]
  private async Task CreateNewBodyMetric()
  {
    DiaryEntry_BodyMetric? result;

    BodyMetric.Acted = ActedDate;
    BodyMetric.Acted.Add(ActedTime);

    if (BodyMetric.Height == default)
    {
      await Shell.Current.DisplayAlert("Invalid entry", "Enter a valid height.", "Close");
      return;
    }
    if (BodyMetric.Weight == default)
    {
      await Shell.Current.DisplayAlert("Invalid entry", "Enter a valid weight.", "Close");
      return;
    }

    result = await _diaryService.AddBodyMetricEntryAsync(BodyMetric);
    if (result is null) return;

    BodyMetric = result;

    await Shell.Current.GoToAsync("..", false, new()
    {
      { "CreatedBodyMetric", BodyMetric }
    });
  }

  [RelayCommand]
  private async Task UpdateBodyMetric()
  {
    DiaryEntry_BodyMetric? success;

    BodyMetric.Acted = ActedDate;
    BodyMetric.Acted.Add(ActedTime);

    if (BodyMetric.Height == default)
    {
      await Shell.Current.DisplayAlert("Invalid entry", "Enter a valid height.", "Close");
      return;
    }
    if (BodyMetric.Weight == default)
    {
      await Shell.Current.DisplayAlert("Invalid entry", "Enter a valid weight.", "Close");
      return;
    }

    success = await _diaryService.AddBodyMetricEntryAsync(BodyMetric);
    if (success is null) return;

    await Shell.Current.GoToAsync("..", false);
  }

  [RelayCommand]
  private async Task DeleteBodyMetric()
  {
    await _diaryService.DeleteBodyMetricEntryAsync(BodyMetric);
    await Shell.Current.GoToAsync("..", false, new()
    {
      { "DeletedBodyMetric", BodyMetric }
    });
  }
}
