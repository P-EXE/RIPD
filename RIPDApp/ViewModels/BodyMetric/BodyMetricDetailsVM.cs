using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(BodyMetric), nameof(BodyMetric))]
public partial class BodyMetricDetailsVM : ObservableObject
{
  [ObservableProperty]
  private BodyMetric _bodyMetric = new()
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

    await Shell.Current.GoToAsync("..", false, new()
    {
      { "CreatedBodyMetric", BodyMetric }
    });
  }

  [RelayCommand]
  private async Task UpdateBodyMetric()
  {
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

    await Shell.Current.GoToAsync("..", false);
  }

  [RelayCommand]
  private async Task DeleteBodyMetric()
  {
    await Shell.Current.GoToAsync("..", false, new()
    {
      { "DeletedBodyMetric", BodyMetric }
    });
  }
}
