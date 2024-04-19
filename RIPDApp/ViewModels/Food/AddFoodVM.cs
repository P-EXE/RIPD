using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDShared.Models;


namespace RIPDApp.ViewModels;

public partial class AddFoodVM : ObservableObject
{
  private readonly IFoodService _foodDataService;

  public AddFoodVM(IFoodService foodDataService)
  {
    _foods = new List<Food>();
    _foodDataService = foodDataService;
  }

  [ObservableProperty]
  private bool _isRefreshing;

  [ObservableProperty]
  private List<Food> _foods;

  [ObservableProperty]
  private Food? _selectedFood;

  [RelayCommand]
  async Task Refresh()
  {
  }

  [RelayCommand]
  async Task ShowDetails(Food food)
  {
    await Shell.Current.GoToAsync($"{nameof(FoodDetailsPage)}?Food={food}");
  }

  [RelayCommand]
  private async void QuickAddFood()
  {

  }

  [RelayCommand]
  private async void NewFood()
  {
    await Shell.Current.GoToAsync($"{nameof(NewFoodPage)}", true);
  }
}
