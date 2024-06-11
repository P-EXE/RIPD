using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDShared.Models;
using System.Collections.ObjectModel;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(ActivePageMode), nameof(PageMode))]
public partial class DiaryVM : ObservableObject
{
  private readonly IDiaryService _diaryService;
  public DiaryVM(IDiaryService diaryService)
  {
    _diaryService = diaryService;
  }

  [ObservableProperty]
  private int _activePageMode;

  [ObservableProperty]
  private DateTime _startDate;
  [ObservableProperty]
  private DateTime _endDate = DateTime.Today;

  [ObservableProperty]
  ObservableCollection<DiaryEntry_Food>? _foodEntries = [];
  [ObservableProperty]
  DiaryEntry_Food? _selectedFoodEntry;
  [ObservableProperty]
  ObservableCollection<DiaryEntry_Workout>? _workoutEntries = [];

  partial void OnActivePageModeChanged(int value)
  {
    switch ((PageMode)value)
    {
      case PageMode.Today:
        {
          StartDate = DateTime.Today;
          break;
        }
      case PageMode.Week:
        {
          StartDate = DateTime.Today.AddDays(-7);
          break;
        }
      case PageMode.Month:
        {
          StartDate = DateTime.Today.AddMonths(-1);
          break;
        }
    }
  }

  private async Task GetFoodEntriesInDateRange()
  {
    IEnumerable<DiaryEntry_Food>? foodEntries = await _diaryService.GetFoodEntriesInDateRange(Statics.Auth.Owner.Diary ,StartDate, EndDate);
    FoodEntries = foodEntries?.ToObservableCollection();
  }

  // Needs to be adjusted to FoodEntry
  [RelayCommand]
  private async Task ShowFoodDetails()
  {
    await Shell.Current.GoToAsync($"{nameof(FoodDetailsPage)}", true, new Dictionary<string, object>
    {
      {"Food", SelectedFoodEntry},
      {"PageMode", FoodDetailsVM.PageMode.View}
    });
    SelectedFoodEntry = null;
  }

  public enum PageMode
  {
    Today = 0,
    Week = 1,
    Month = 2,
  }
}
