﻿using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDShared.Models;
using System.Collections.ObjectModel;


namespace RIPDApp.ViewModels;

public partial class AddFoodVM : ObservableObject
{
  private readonly IFoodService _foodService;

  public AddFoodVM(IFoodService foodService)
  {
    _foodService = foodService;
  }

  [ObservableProperty]
  private bool _isRefreshing;
  [ObservableProperty]
  private string _searchText;
  [ObservableProperty]
  private ObservableCollection<Food>? _foods;
  [ObservableProperty]
  private Food? _selectedFood;


  [RelayCommand]
  async Task Search()
  {
    IEnumerable<Food>? foods = await _foodService.GetFoodsByNameAtPositionAsync(SearchText, 0);
    Foods = foods?.ToObservableCollection();
  }

  [RelayCommand]
  async Task Refresh()
  {
  }

  [RelayCommand]
  async Task ShowDetails()
  {
    await Shell.Current.GoToAsync($"{nameof(FoodDetailsPage)}", true, new Dictionary<string, object>
    {
      [nameof(Food)] = SelectedFood
    });
    SelectedFood = null;
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
