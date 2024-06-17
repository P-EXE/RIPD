using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(Food), nameof(Food))]
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

  [RelayCommand]
  private async Task AddFoodEntryToDiary()
  {
    FoodEntry.FoodId = Food.Id;
    FoodEntry.Food = Food;
    await _diaryService.AddFoodEntryToDiaryAsync(FoodEntry);
  }
}
