using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    [ObservableProperty]
    private List<Food> _foods;

    public AddFoodVM()
    {
      _foods = new List<Food>();
      _foods.Add(new Food() { Id = 0, Name = "TestFood 1", Manufacturer = 0, CreationDateTime = DateTime.Now });
      _foods.Add(new Food() { Id = 1, Name = "TestFood 2", Manufacturer = 0, CreationDateTime = DateTime.Now.AddDays(1) });
      _foods.Add(new Food() { Id = 2, Name = "TestFood 3", Manufacturer = 1, CreationDateTime = DateTime.Now.AddDays(2) });
    }

    [RelayCommand]
    async Task ShowDetails(Food food)
    {
      Debug.WriteLine($"Show Food Details for: {food.Id}");
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
      Debug.WriteLine("Open new Food Page");
    }
  }
}
