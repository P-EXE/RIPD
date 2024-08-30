using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(Food), nameof(Food))]
[QueryProperty(nameof(FoodEntry), nameof(FoodEntry))]
[QueryProperty(nameof(Workout), nameof(Workout))]
[QueryProperty(nameof(WorkoutEntry), nameof(WorkoutEntry))]
[QueryProperty(nameof(ActivePageMode), nameof(PageMode))]
public partial class DiaryEntryVM : ObservableObject
{
  private readonly IDiaryService _diaryService;
  public DiaryEntryVM(IDiaryService diaryService)
  {
    _diaryService = diaryService;
  }

  [ObservableProperty]
  private int _activePageMode;
  [ObservableProperty]
  private bool _pageModeCreate;
  [ObservableProperty]
  private bool _pageModeUpdate;
  [ObservableProperty]
  private bool _pageModeDelete;

  [ObservableProperty]
  private DateTime _actedDate = DateTime.UtcNow;
  [ObservableProperty]
  private DateTime _actedTime = DateTime.UtcNow;

  partial void OnActivePageModeChanged(int value)
  {
    switch ((PageMode)value)
    {
      case PageMode.Create:
        {
          PageModeCreate = true;
          PageModeUpdate = false;
          PageModeDelete = false;
          break;
        }
      case PageMode.Update:
        {
          PageModeCreate = false;
          PageModeUpdate = true;
          PageModeDelete = true;
          break;
        }
      case PageMode.Delete:
        {
          PageModeCreate = false;
          PageModeUpdate = true;
          PageModeDelete = true;
          break;
        }
    }
  }

  [ObservableProperty]
  private Food _food = new();
  [ObservableProperty]
  private DiaryEntry_Food _foodEntry = new()
  {
    DiaryId = Statics.Auth.Owner.Diary.OwnerId,
    Diary = Statics.Auth.Owner.Diary
  };

  [ObservableProperty]
  private Workout _workout = new();
  [ObservableProperty]
  private DiaryEntry_Workout _workoutEntry = new()
  {
    DiaryId = Statics.Auth.Owner.Diary.OwnerId,
    Diary = Statics.Auth.Owner.Diary
  };

  [RelayCommand]
  private async Task AddFoodEntryToDiary()
  {
    FoodEntry.FoodId = Food.Id;
    FoodEntry.Food = Food;
    FoodEntry.Acted = ActedDate.Add(ActedTime.TimeOfDay);

    bool success = default != await _diaryService.AddFoodEntryAsync(FoodEntry);
    if (!success)
      return;
    await GoBack();
  }

  [RelayCommand]
  private async Task AddWorkoutEntryToDiary()
  {
    WorkoutEntry.WorkoutId = Workout.Id;
    WorkoutEntry.Workout = Workout;
    WorkoutEntry.Acted = ActedDate.Add(ActedTime.TimeOfDay);
    bool success = default != await _diaryService.AddWorkoutEntryAsync(WorkoutEntry);
    if (!success)
      return;
    await GoBack();
  }

  [RelayCommand]
  private async Task UpdateFoodEntry()
  {
    FoodEntry.FoodId = Food.Id;
    FoodEntry.Food = Food;
    FoodEntry.Acted = ActedDate.Add(ActedTime.TimeOfDay);
    bool success = default != await _diaryService.UpdateFoodEntryAsync(FoodEntry);
    if (!success)
      return;
    await GoBack();
  }

  [RelayCommand]
  private async Task UpdateWorkoutEntry()
  {
    WorkoutEntry.WorkoutId = Workout.Id;
    WorkoutEntry.Workout = Workout;
    WorkoutEntry.Acted = ActedDate.Add(ActedTime.TimeOfDay);
    bool success = default != await _diaryService.UpdateWorkoutEntryAsync(WorkoutEntry);
    if (!success)
      return;
    await GoBack();
  }

  [RelayCommand]
  private async Task DeleteFoodEntryFromDiary()
  {
    await _diaryService.DeleteFoodEntryAsync(FoodEntry);
    await Shell.Current.GoToAsync("..", false, new()
    {
      { "DeletedFoodEntry", FoodEntry }
    });
  }

  [RelayCommand]
  private async Task DeleteWorkoutEntryFromDiary()
  {
    await _diaryService.DeleteWorkoutEntryAsync(WorkoutEntry);
    await Shell.Current.GoToAsync("..", false, new()
    {
      { "DeletedWorkoutEntry", WorkoutEntry }
    });
  }

  private async Task GoBack()
  {
    await Shell.Current.GoToAsync("..", true);
  }

  public enum PageMode
  {
    Create,
    Update,
    Delete
  }
}

