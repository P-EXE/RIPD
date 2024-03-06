using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.DataServices;
using RIPD.Models;
using RIPD.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  public partial class AddFoodVM : ObservableObject
  {
    private readonly IFoodDataService _foodDataService;

    [ObservableProperty]
    private bool _isRefreshing;

    [ObservableProperty]
    private List<Food> _foods;

    public AddFoodVM(IFoodDataService foodDataService)
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
        Foods = await _foodDataService.GetAllFoodsAsync();
      }
      catch (Exception ex)
      {

      }
      finally { IsRefreshing = false; }
    }

    [RelayCommand]
    async Task ShowDetails(Food food)
    {
      /*      await Shell.Current.GoToAsync(nameof(FoodDetailsPage),
      new Dictionary<string, object>
      {
              { nameof(food), food }
      });*/
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
}
