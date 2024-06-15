using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDApp.Collections;
using RIPDShared.Models;
using CommunityToolkit.Mvvm.Messaging;
using RIPDApp.Messaging;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(Food), nameof(Food))]
[QueryProperty(nameof(ActivePageMode), nameof(PageMode))]
public partial class FoodDetailsVM : ObservableObject
{
  private readonly IFoodService _foodService;
  public FoodDetailsVM(IFoodService foodService)
  {
    _foodService = foodService;

    WeakReferenceMessenger.Default.Register<PageReturnObjectMessage<AppUser>>(this, (r, m) =>
    {
      SetManufacturer(m.Value);
    });
    WeakReferenceMessenger.Default.Register<PageReturnObjectMessage<string>>(this, (r, m) =>
    {
      SetBarcode(m.Value);
    });
  }

  // Page State Fields
  [ObservableProperty]
  private int _activePageMode;
  [ObservableProperty]
  private bool _pageModeView;
  [ObservableProperty]
  private bool _pageModeUpdate;
  [ObservableProperty]
  private bool _pageModeCreate;

  // Availability
  [ObservableProperty]
  private bool _available;

  // Displayed Fields
  [ObservableProperty]
  private Food? _food = new();
  [ObservableProperty]
  private string _manufacturerUserName = "Search Manufacturer";
  [ObservableProperty]
  private string _barcode = "Scan Barcode";

  // Experimental
  [ObservableProperty]
  ObservablePropertyCollection<Food> _foodProperties = [];

  [ObservableProperty]
  private DateTime _acted = DateTime.Now;

  async partial void OnActivePageModeChanged(int value)
  {
    switch ((PageMode)value)
    {
      case PageMode.View:
        {
          PageModeView = true;
          PageModeUpdate = false;
          PageModeCreate = false;
          break;
        }
      case PageMode.Update:
        {
          PageModeView = false;
          PageModeUpdate = true;
          PageModeCreate = false;
          break;
        }
      case PageMode.Create:
        {
          PageModeView = false;
          PageModeUpdate = false;
          PageModeCreate = true;
          break;
        }
    }
  }

  // Sets the Manufacturer field
  private void SetManufacturer(AppUser manufacturer)
  {
    Food.Manufacturer = manufacturer;
    ManufacturerUserName = manufacturer.UserName ?? "Search Manufacturer";
  }

  // Sets the Barcode field
  private void SetBarcode(string barcode)
  {
    Food.Barcode = barcode;
    Barcode = barcode ?? "Scan Barcode";
  }

  [RelayCommand]
  private async Task ScanBarcode()
  {
    await Shell.Current.GoToAsync(nameof(BarcodeScannerPage));
  }

  [RelayCommand]
  private async Task GetUser()
  {
    await Shell.Current.GoToAsync($"{nameof(UserSearchPage)}", true, new Dictionary<string, object>()
    {
      { "PageMode", UserSearchVM.PageMode.Return }
    });
  }

  [RelayCommand]
  async Task GoBack() => await Shell.Current.GoToAsync("..");

  [RelayCommand]
  private async Task CreateFood()
  {
    Food = await _foodService.CreateFoodAsync(Food);
    GoBack();
  }

  [RelayCommand]
  private async Task UpdateFood()
  {
  }

  #region Switch methods

  [RelayCommand]
  private async Task SwitchToViewMode()
  {
    ActivePageMode = (int)PageMode.View;
    await Shell.Current.GoToAsync($"{nameof(FoodViewPage)}", false, new()
    {
    });
  }

  [RelayCommand]
  private async Task SwitchToUpdateMode()
  {
    ActivePageMode = (int)PageMode.Update;
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
    Update = 2,
    Create = 3,
  }
}