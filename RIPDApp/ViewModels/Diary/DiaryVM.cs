using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using Microcharts.Maui;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDShared.Models;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(ActivePageMode), nameof(PageMode))]
[QueryProperty(nameof(DeletedFoodEntry), nameof(DeletedFoodEntry))]
[QueryProperty(nameof(DeletedWorkoutEntry), nameof(DeletedWorkoutEntry))]
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
  // Maybe add 23h59m59s
  [ObservableProperty]
  private DateTime _endDate = DateTime.Today;

  [ObservableProperty]
  private ObservableCollection<DiaryEntry_Food> _foodEntries = [];
  [ObservableProperty]
  private DiaryEntry_Food? _selectedFoodEntry;
  [ObservableProperty]
  private DiaryEntry_Food? _deletedFoodEntry;
  [ObservableProperty]
  private ObservableCollection<DiaryEntry_Workout> _workoutEntries = [];
  [ObservableProperty]
  private DiaryEntry_Workout? _selectedWorkoutEntry;
  [ObservableProperty]
  private DiaryEntry_Workout? _deletedWorkoutEntry;

  #region Charts
  private const int CornerRadius = 1000;
  private static readonly SKColor bgColor = SKColor.Parse("#00000000");
  private static readonly SKColor posColor = SKColor.Parse("#2000FF00");
  private static readonly SKColor negColor = SKColor.Parse("#20FF0000");
  [ObservableProperty]
  private BarChart _positive = new()
  {
    Entries = _posEntries,
    CornerRadius = 10,
    BackgroundColor = bgColor,
    ValueLabelOption = ValueLabelOption.None,
    BarAreaAlpha = 0
  };
  [ObservableProperty]
  private BarChart _negative = new()
  {
    Entries = _negEntries,
    CornerRadius = 10,
    BackgroundColor = bgColor,
    ValueLabelOption = ValueLabelOption.None,
    BarAreaAlpha = 0
  };

  private static readonly ObservableCollection<ChartEntry> _posEntries = [
    new(0.1f){Color = posColor},
    new(0.2f){Color = posColor},
    new(0.3f){Color = posColor},
    new(0.4f){Color = posColor},
    new(0.5f){Color = posColor},
    new(0.6f){Color = posColor},
    new(0.7f){Color = posColor}
    ];

  private static readonly ObservableCollection<ChartEntry> _negEntries = [
    new(0.1f){Color = negColor},
    new(0.2f){Color = negColor},
    new(0.3f){Color = negColor},
    new(0.4f){Color = negColor},
    new(0.5f){Color = negColor},
    new(0.6f){Color = negColor},
    new(0.7f){Color = negColor}
  ];
  #endregion Charts

  partial void OnActivePageModeChanged(int value)
  {
    switch ((PageMode)value)
    {
      case PageMode.Today:
        {
          StartDate = DateTime.Today;
          // Maybe add 23h59m59s
          EndDate = DateTime.Today;
          break;
        }
      case PageMode.Week:
        {
          StartDate = DateTime.Today.AddDays(-7);
          // Maybe add 23h59m59s
          EndDate = DateTime.Today;
          break;
        }
      case PageMode.Month:
        {
          StartDate = DateTime.Today.AddMonths(-1);
          // Maybe add 23h59m59s
          EndDate = DateTime.Today;
          break;
        }
    }
  }

  partial void OnDeletedFoodEntryChanged(DiaryEntry_Food? value)
  {
    if (value == null)
    {
      Shell.Current.DisplayAlert("Error", "Unable to remove Food Entry.", "Close");
      return;
    }
    FoodEntries.Remove(value);
  }

  partial void OnDeletedWorkoutEntryChanged(DiaryEntry_Workout? value)
  {
    if (value == null)
    {
      Shell.Current.DisplayAlert("Error", "Unable to remove Workout Entry.", "Close");
      return;
    }
    WorkoutEntries.Remove(value);
  }


  [RelayCommand]
  private async Task Refresh()
  {
    IEnumerable<DiaryEntry_Food>? foodEntries = await _diaryService.GetFoodEntriesAsync(Statics.Auth.Owner.Diary, StartDate, EndDate);
    FoodEntries = foodEntries?.ToObservableCollection();
    IEnumerable<DiaryEntry_Workout>? workoutEntries = await _diaryService.GetWorkoutEntriesAsync(Statics.Auth.Owner.Diary, StartDate, EndDate);
    WorkoutEntries = workoutEntries?.ToObservableCollection();

    // TODO: Perform some kind of transformation for display
  }

  [RelayCommand]
  private async Task ShowFoodDetails()
  {
    await Shell.Current.GoToAsync($"{Routes.DiaryEntryFoodEditPage}", true, new Dictionary<string, object>
    {
      {"FoodEntry", SelectedFoodEntry},
      {"Food", SelectedFoodEntry.Food}
    });
    SelectedFoodEntry = null;
  }

  [RelayCommand]
  private async Task ShowWorkoutDetails()
  {
    await Shell.Current.GoToAsync($"{Routes.DiaryEntryWorkoutEditPage}", true, new Dictionary<string, object>
    {
      {"WorkoutEntry", SelectedWorkoutEntry},
      {"Workout", SelectedWorkoutEntry.Workout}
    });
    SelectedWorkoutEntry = null;
  }

  public enum PageMode
  {
    Today = 0,
    Week = 1,
    Month = 2,
  }
}
