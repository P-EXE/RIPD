using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDApp.Pages;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

[QueryProperty("Manufacturer", "Manufacturer")]
public partial class NewFoodVM : ObservableObject
{
  private readonly IFoodService _foodService;

  public NewFoodVM(IFoodService foodService)
  {
    _foodService = foodService;
  }

  [ObservableProperty]
  private bool _available = true;
  [ObservableProperty]
  private string? _barcode;
  [ObservableProperty]
  private string? _name;
  [ObservableProperty]
  private AppUser? _manufacturer;
  [ObservableProperty]
  private string? _description;
  [ObservableProperty]
  private string? _image;

  [RelayCommand]
  private async Task CreateNewFood()
  {
    bool success = false;
    Available = false;
    try
    {
      FoodDTO_Create createFood = new()
      {
        Barcode = Barcode,
        Name = Name,
        ManufacturerId = Manufacturer.Id,
        Description = Description,
        Image = Image
      };
      success = await _foodService.CreateFoodAsync(createFood);
    }
    catch (Exception ex)
    {

    }
    if (success) await GoBack();
    Available = true;
  }

  [RelayCommand]
  async Task SearchManufacturer ()
  {
    await Shell.Current.GoToAsync($"{nameof(UserSearchPage)}", true);
  }

  [RelayCommand]
  async Task GoBack() => await Shell.Current.GoToAsync("..");
}