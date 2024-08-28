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
  ObservableCollection<DiaryEntry_Food>? _foodEntries = [];
  [ObservableProperty]
  DiaryEntry_Food? _selectedFoodEntry;
  [ObservableProperty]
  ObservableCollection<DiaryEntry_Workout>? _workoutEntries = [];

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

  private async Task GetFoodEntriesInDateRange()
  {
    IEnumerable<DiaryEntry_Food>? foodEntries = await _diaryService.GetFoodEntriesAsync(Statics.Auth.Owner.Diary, StartDate, EndDate);
    FoodEntries = foodEntries?.ToObservableCollection();
  }

  // Needs to be adjusted to FoodEntryPage
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
