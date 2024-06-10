using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDApp.Pages;
using RIPDShared.Models;
using CommunityToolkit.Mvvm.Messaging;
using RIPDApp.Messaging;

namespace RIPDApp.ViewModels;

public partial class NewFoodVM : ObservableObject
{
  private readonly IFoodService _foodService;

  public NewFoodVM(IFoodService foodService)
  {
    _foodService = foodService;

    WeakReferenceMessenger.Default.Register<PageReturnObjectMessage<AppUser>>(this, (r, m) =>
    {
      SetManufacturer(m.Value);
    });
  }

  [ObservableProperty]
  private bool _available = true;

  [ObservableProperty]
  private Food _food = new()
  {
    Manufacturer = null,
    ManufacturerId = null,
    Contributer = Statics.Auth.Owner,
    ContributerId = Statics.Auth.Owner?.Id
  };
  [ObservableProperty]
  private string _manufacturerName = "search Manufacturer";

  // Sets the Manufacturer field
  private void SetManufacturer(AppUser manufacturer)
  {
    Food.Manufacturer = manufacturer;
    ManufacturerName = manufacturer.UserName;
  }

  [RelayCommand]
  private async Task CreateNewFood()
  {
    await _foodService.CreateFoodAsync(Food);
    await GoBack();
  }

  // Opens the UserSearchPage in return mode to get a User for the Manufacturer field
  [RelayCommand]
  async Task SearchManufacturer()
  {
    await Shell.Current.GoToAsync($"{nameof(UserSearchPage)}", true, new Dictionary<string, object>()
    {
      { "PageMode", UserSearchVM.PageMode.Return }
    });
  }

  [RelayCommand]
  async Task GoBack() => await Shell.Current.GoToAsync("..");
}