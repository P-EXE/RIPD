using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDApp.Collections;
using RIPDShared.Models;
using System.Reflection;
using System.Diagnostics;

namespace RIPDApp.ViewModels;

[QueryProperty("Food", "Food")]
[QueryProperty(nameof(ActivePageMode), nameof(PageMode))]
public partial class FoodDetailsVM : ObservableObject
{
  private readonly IFoodService _foodService;
  public FoodDetailsVM(IFoodService foodService)
  {
    _foodService = foodService;
  }

  // Page State Fields
  [ObservableProperty]
  private int _activePageMode;
  [ObservableProperty]
  private bool _pageModeView;
  [ObservableProperty]
  private bool _pageModeEdit;
  [ObservableProperty]
  private bool _pageModeCreate;

  // Availability
  [ObservableProperty]
  private bool _available;

  // Displayed Fields
  [ObservableProperty]
  private Food _food = new();

  [ObservableProperty]
  ObservablePropertyCollection<Food> _foodProperties = [];

  [ObservableProperty]
  private double _amount;
  [ObservableProperty]
  private DateTime _acted = DateTime.Now;

  async partial void OnActivePageModeChanged(int value)
  {
    switch ((PageMode)value)
    {
      case PageMode.View:
        {
          PageModeView = true;
          PageModeEdit = false;
          PageModeCreate = false;
          break;
        }
      case PageMode.Edit:
        {
          PageModeView = false;
          PageModeEdit = true;
          PageModeCreate = false;
          break;
        }
      case PageMode.Create:
        {
          PageModeView = false;
          PageModeEdit = false;
          PageModeCreate = true;
          FoodProperties.Disassemble(Food);
          break;
        }
    }
  }

  [RelayCommand]
  private async Task ScanBarcode()
  {
    await Shell.Current.GoToAsync(nameof(BarcodeScannerPage));
  }

  [RelayCommand]
  async Task GoBack() => await Shell.Current.GoToAsync("..");

  [RelayCommand]
  private async Task CreateFood()
  {
    Food? createFood = FoodProperties.Assemble();
    foreach(PropertyInfo propInfo in typeof(Food).GetProperties())
    {
      Debug.WriteLine($"{propInfo.Name} : {propInfo.GetValue(createFood) ?? "Was Null"}");
    }
  }

  #region Switch methods

  [RelayCommand]
  private async Task SwitchToViewMode()
  {
    ActivePageMode = (int)PageMode.View;
  }

  [RelayCommand]
  private async Task SwitchToEditMode()
  {
    ActivePageMode = (int)PageMode.Edit;
  }

  [RelayCommand]
  private async Task SwitchToCreateMode()
  {
    ActivePageMode = (int)PageMode.Create;
  }

  #endregion Switch methods

  public enum PageMode
  {
    Default = 0,
    View = 1,
    Edit = 2,
    Create = 3,
  }

  /*  [RelayCommand]
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
      success = await _foodService.AddFoodEntryToDiaryAsync(entry);
    }
    catch (Exception ex)
    {

    }
    if (success)
      await GoBack();
    Available = true;
  }*/
}