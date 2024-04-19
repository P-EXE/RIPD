using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDShared.Models;


namespace RIPDApp.ViewModels;

public partial class AddFoodVM : ObservableObject
{
  private readonly IFoodService _foodDataService;

  [ObservableProperty]
  private bool _isRefreshing;

  [ObservableProperty]
  private List<Food> _foods;

  public AddFoodVM(IFoodService foodDataService)
  {
    _foods = new List<Food>();
    _foodDataService = foodDataService;
  }

  [RelayCommand]
  async Task Refresh()
  {
    try
    {
      Foods.Clear();
      /*Foods = await _foodDataService.GetMultipleAsync();*/
    }
    catch (Exception ex)
    {

    }
    finally { IsRefreshing = false; }
  }

  [RelayCommand]
  async Task ShowDetails(Food food)
  {
    await Shell.Current.GoToAsync($"{nameof(FoodDetailsPage)}?Food={food}");
  }

  [RelayCommand]
  private async void AddFood()
  {

  }

  [RelayCommand]
  private async void NewFood()
  {
    await Shell.Current.GoToAsync($"{nameof(NewFoodPage)}", true);
  }
}
