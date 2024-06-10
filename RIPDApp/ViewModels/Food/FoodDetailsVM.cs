using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDApp.Tools;
using RIPDShared.Models;
using System.Reflection;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(ActivePageMode), "PageMode")]
[QueryProperty("Food", "Food")]
public partial class FoodDetailsVM : ObservableObject
{
  private readonly IDiaryService _diaryService;
  public FoodDetailsVM(IDiaryService diaryService)
  {
    _diaryService = diaryService;
  }

  [ObservableProperty]
  private int _activePageMode;
  [ObservableProperty]
  private bool _addEntryMode;


  [ObservableProperty]
  private bool _available;
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(FoodProperties))]
  private Food? _food;
  [ObservableProperty]
  private int _amount;
  [ObservableProperty]
  private DateTime _acted;

  public Props<Food>? FoodProperties => new(Food);

  partial void OnActivePageModeChanged(int value)
  {
    switch ((PageMode)value)
    {
      case PageMode.View:
        {
          break;
        }
        case PageMode.Edit:
        {
          break;
        }
        case PageMode.AddEntry:
        {
          AddEntryMode = true;
          break;
        }
    }
  }

  [RelayCommand]
  private async Task AddFoodToDiary()
  {
    bool success = false;
    Available = false;
    try
    {
      DiaryEntry_Food entry = new()
      {
        Acted = Acted,
        Added = DateTime.Now,
        Amount = Amount,
        Diary = Statics.Auth.Owner.Diary,
        DiaryId = Statics.Auth.Owner.DiaryId,
        EntryNr = Statics.Auth.Owner.Diary.FoodEntries.Count + 1,
        Food = Food,
        FoodId = Food.Id
      };
      success = await _diaryService.AddFoodToDiaryAsync(entry);
    }
    catch (Exception ex)
    {

    }
    if (success) await GoBack();
    Available = true;
  }

  [RelayCommand]
  async Task GoBack() => await Shell.Current.GoToAsync("..");

  public enum PageMode
  {
    View = 0,
    Edit = 1,
    AddEntry = 2,
  }
}