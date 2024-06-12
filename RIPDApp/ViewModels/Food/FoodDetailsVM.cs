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
  private Props _viewProperties;
  [ObservableProperty]
  private Props _updateProperties;
  [ObservableProperty]
  private Props _createProperties;

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
          ViewProperties = new(Food, false);
          break;
        }
      case PageMode.Edit:
        {
          PageModeView = false;
          PageModeEdit = true;
          PageModeCreate = false;
          UpdateProperties = new(Food, typeof(Food_Update));
          break;
        }
      case PageMode.Create:
        {
          PageModeView = false;
          PageModeEdit = false;
          PageModeCreate = true;
          CreateProperties = new(typeof(Food_Create), new()
          {
            nameof(Food_Create.Image),
            nameof(Food_Create.Name),
            nameof(Food_Create.Barcode),
            nameof(Food_Create.Description),

            nameof(Food_Create.ManufacturerId),
            nameof(Food_Create.ContributerId),
          });
          break;
        }
    }
  }

  [RelayCommand]
  private async Task ScanBarcode()
  {
    await Shell.Current.GoToAsync(nameof());
  }

  [RelayCommand]
  async Task GoBack() => await Shell.Current.GoToAsync("..");

  [RelayCommand]
  private async Task CreateFood()
  {
    Food_Create createFood = CreateProperties.Recombine<Food_Create>();
    ActivePageMode = (int)PageMode.View;
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
      success = await _diaryService.AddFoodEntryToDiaryAsync(entry);
    }
    catch (Exception ex)
    {

    }
    if (success)
      await GoBack();
    Available = true;
  }*/
}