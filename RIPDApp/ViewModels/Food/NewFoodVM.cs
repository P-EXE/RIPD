using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDShared.Models;
using System.Diagnostics;

namespace RIPDApp.ViewModels;

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
  private string? _manufacturer;
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
        ManufacturerId = new(Manufacturer),
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
  async Task GoBack() => await Shell.Current.GoToAsync("..");
}