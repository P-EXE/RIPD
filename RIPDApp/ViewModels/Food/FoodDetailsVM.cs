using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDApp.Tools;
using RIPDShared.Models;
using System.Reflection;

namespace RIPDApp.ViewModels;

[QueryProperty("Food", "Food")]
[QueryProperty(nameof(ActivePageMode), nameof(PageMode))]
public partial class FoodDetailsVM : ObservableObject
{
  private readonly IDiaryService _diaryService;
  public FoodDetailsVM(IDiaryService diaryService)
  {
    _diaryService = diaryService;
  }

  // Page State Fields
  [ObservableProperty]
  private int _activePageMode;
  [ObservableProperty]
  [NotifyPropertyChangedFor(nameof(PageModeView))]
  private bool _pageModeEdit;
  public bool PageModeView => !PageModeEdit;

  // Availability
  [ObservableProperty]
  private bool _available;

  // Displayed Fields
  [ObservableProperty]
  private Food _food = new();
  [ObservableProperty]
  private Props<Food>? _foodProperties;

  [ObservableProperty]
  private double _amount;
  [ObservableProperty]
  private DateTime _acted = DateTime.Now;

  async partial void OnActivePageModeChanged(int value)
  {
    FoodProperties = null;
    switch ((PageMode)value)
    {
      case PageMode.View:
        {
          PageModeEdit = false;
          FoodProperties = new(Food, false, new()
          {
            // Beauty
            nameof(Food.Name),
            nameof(Food.Manufacturer),
            nameof(Food.Description),
            // Ids
            nameof(Food.Id),
            nameof(Food.ManufacturerId),
            nameof(Food.ContributerId),
            nameof(Food.CreationDateTime),
            nameof(Food.UpdateDateTime),
          });
          break;
        }
      case PageMode.Edit:
        {
          PageModeEdit = true;
          FoodProperties = new(Food, true);
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
        DiaryId = Statics.Auth.Owner.Diary.OwnerId,
        EntryNr = Statics.Auth.Owner.Diary.FoodEntries.Count + 1,
        Food = Food,
        FoodId = Food.Id
      };
      success = await _diaryService.AddFoodEntryToDiaryAsync(entry);
    }
    catch (Exception ex)
    {

    }
    if (success)
      await GoBack();
    Available = true;
  }

  [RelayCommand]
  async Task GoBack() => await Shell.Current.GoToAsync("..");

  [RelayCommand]
  private async Task SwitchToEditMode()
  {
    ActivePageMode = (int)PageMode.Edit;
  }

  [RelayCommand]
  private async Task SwitchToViewMode()
  {
    ActivePageMode = (int)PageMode.View;
  }

  public enum PageMode
  {
    Default = 0,
    View = 1,
    Edit = 2
  }
}