using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(Food), nameof(Food))]
[QueryProperty(nameof(Workout), nameof(Workout))]
public partial class DiaryEntryVM : ObservableObject
{
  private readonly IDiaryService _diaryService;
  public DiaryEntryVM(IDiaryService diaryService)
  {
    _diaryService = diaryService; 
  }

  [ObservableProperty]
  private Food _food = new();
  [ObservableProperty]
  private DiaryEntry_Food _foodEntry = new()
  {
    Acted = DateTime.Now,
    Added = DateTime.Now,
    DiaryId = Statics.Auth.Owner.Diary.OwnerId,
    Diary = Statics.Auth.Owner.Diary
  };

  [ObservableProperty]
  private Workout _workout = new();
  [ObservableProperty]
  private DiaryEntry_Workout _workoutEntry = new()
  {
    Acted = DateTime.Now,
    Added = DateTime.Now,
    DiaryId = Statics.Auth.Owner.Diary.OwnerId,
    Diary = Statics.Auth.Owner.Diary
  };

  [RelayCommand]
  private async Task AddFoodEntryToDiary()
  {
    FoodEntry.FoodId = Food.Id;
    FoodEntry.Food = Food;
    bool success = await _diaryService.AddFoodEntryToDiaryAsync(FoodEntry);
    if (!success)
      return;
    await GoBack();
  }

  [RelayCommand]
  private async Task AddWorkoutEntryToDiary()
  {
    WorkoutEntry.WorkoutId = Workout.Id;
    WorkoutEntry.Workout = Workout;
    bool success = await _diaryService.AddWorkoutEntryToDiaryAsync(WorkoutEntry);
    if (!success)
      return;
    await GoBack();
  }

  private async Task GoBack()
  {
    await Shell.Current.GoToAsync("..", true);
  }
}
