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

 

  private static readonly ObservableCollection<ChartEntry> _weekconsumedcalories = [
    new(3400){ ValueLabel = "3400",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White},
    new(3500){ ValueLabel = "3500",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White},
    new(4400){ ValueLabel = "4400",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White},
    new(2400){ ValueLabel = "2400",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White},
    new(1900){ ValueLabel = "1900",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White},
    new(3600){ ValueLabel = "3600",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White},
    new(2400){ ValueLabel = "2400",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White},
    ];

  private static readonly ObservableCollection<ChartEntry> _weekburnedcalories = [
    new(-3400){ ValueLabel = "3400",Color = SKColors.Red,ValueLabelColor = SKColors.White},
    new(-3000){ ValueLabel = "3000",Color = SKColors.Red,ValueLabelColor = SKColors.White},
    new(-2000){ ValueLabel = "2000",Color = SKColors.Red,ValueLabelColor = SKColors.White},
    new(-1400){ ValueLabel = "1400",Color = SKColors.Red,ValueLabelColor = SKColors.White},
    new(-1900){ ValueLabel = "1900",Color = SKColors.Red,ValueLabelColor = SKColors.White},
    new(-4000){ ValueLabel = "4000",Color = SKColors.Red,ValueLabelColor = SKColors.White},
    new(-1200){ ValueLabel = "1200",Color = SKColors.Red,ValueLabelColor = SKColors.White}
    ];

  private static readonly ObservableCollection<ChartEntry> _monthconsumedcalories = [
    new(3400){ ValueLabel = "3400",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White},
    new(3500){ ValueLabel = "3500",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White},
    new(4400){ ValueLabel = "4400",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White},
    new(2400){ ValueLabel = "2400",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White}
    ];

  private static readonly ObservableCollection<ChartEntry> _monthburnedcalories = [
    new(-3400){ ValueLabel = "3400",Color = SKColors.Red,ValueLabelColor = SKColors.White},
    new(-3000){ ValueLabel = "3000",Color = SKColors.Red,ValueLabelColor = SKColors.White},
    new(-2000){ ValueLabel = "2000",Color = SKColors.Red,ValueLabelColor = SKColors.White},
    new(-1400){ ValueLabel = "1400",Color = SKColors.Red,ValueLabelColor = SKColors.White}
    ];

  private static readonly ObservableCollection<ChartEntry> _dayconsumedcalories = [
    new(4000){ ValueLabel = "4000",Color = SKColors.LimeGreen,ValueLabelColor = SKColors.White}
    ];

  private static readonly ObservableCollection<ChartEntry> _dayburnedcalories = [
    new(-2500){ ValueLabel = "2500",Color = SKColors.Red,ValueLabelColor = SKColors.White}
    ];

  [ObservableProperty]
  private PointChart _weekChartView = new PointChart
  {
    //BackgroundColor = SKColor.Parse("#172610"),
    BackgroundColor = SKColors.Black,
    LabelOrientation = Orientation.Horizontal,
    ValueLabelOption = ValueLabelOption.TopOfElement,
    ValueLabelOrientation = Orientation.Horizontal,

    Entries = _weekconsumedcalories
  };
  [ObservableProperty]
  private PointChart _weekChartView1 = new PointChart
  {
    //BackgroundColor = SKColor.Parse("#172610"),
    BackgroundColor = SKColors.Black,
    LabelOrientation = Orientation.Horizontal,
    ValueLabelOption = ValueLabelOption.TopOfElement,
    ValueLabelOrientation = Orientation.Horizontal,
    

    Entries = _weekburnedcalories
  };

  [ObservableProperty]
  private PointChart _monthChartView = new PointChart
  {
    BackgroundColor = SKColors.Black,
    LabelOrientation = Orientation.Horizontal,
    ValueLabelOption = ValueLabelOption.TopOfElement,
    ValueLabelOrientation = Orientation.Horizontal,

    Entries = _monthconsumedcalories
  };

  [ObservableProperty]
  private PointChart _monthChartView1 = new PointChart
  {
    //BackgroundColor = SKColor.Parse("#172610"),
    BackgroundColor = SKColors.Black,
    LabelOrientation = Orientation.Horizontal,
    ValueLabelOption = ValueLabelOption.TopOfElement,
    ValueLabelOrientation = Orientation.Horizontal,


    Entries = _monthburnedcalories
  };

  [ObservableProperty]
  private PointChart _todayChartView = new PointChart
  {
    BackgroundColor = SKColors.Black,
    LabelOrientation = Orientation.Horizontal,
    ValueLabelOption = ValueLabelOption.TopOfElement,
    ValueLabelOrientation = Orientation.Horizontal,

    Entries = _dayconsumedcalories
  };

  [ObservableProperty]
  private PointChart _todayChartView1 = new PointChart
  {
    //BackgroundColor = SKColor.Parse("#172610"),
    BackgroundColor = SKColors.Black,
    LabelOrientation = Orientation.Horizontal,
    ValueLabelOption = ValueLabelOption.TopOfElement,
    ValueLabelOrientation = Orientation.Horizontal,


    Entries = _dayburnedcalories
  };

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
