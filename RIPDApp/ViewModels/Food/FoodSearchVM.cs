using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RIPDApp.Messaging;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDShared.Models;
using System.Collections.ObjectModel;

namespace RIPDApp.ViewModels;

public partial class FoodSearchVM : ObservableObject
{
  private readonly IFoodService _foodService;

  public FoodSearchVM(IFoodService foodService)
  {
    _foodService = foodService;
    WeakReferenceMessenger.Default.Register<PageReturnObjectMessage<string>>(this, (r, m) =>
    {
      SearchText = m.Value;
    });
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
    try
    {
      IEnumerable<Food>? foods = await _foodService.GetFoodsByNameAtPositionAsync(SearchText, 0);
      Foods = foods?.ToObservableCollection();
    }
    catch (HttpRequestException ex)
    {
      
    }
  }
  [RelayCommand]
  private async Task SearchViaBarcode()
  {
    await Shell.Current.GoToAsync($"{nameof(BarcodeScannerPage)}");
  }

  [RelayCommand]
  async Task Refresh()
  {
  }

  [RelayCommand]
  private async void QuickAddFood()
  {

  }

  [RelayCommand]
  async Task ShowDetails()
  {
    await Shell.Current.GoToAsync($"{nameof(FoodViewPage)}", true, new Dictionary<string, object>
    {
      {"Food", SelectedFood},
      {"PageMode", FoodDetailsVM.PageMode.View}
    });
    SelectedFood = null;
  }

  [RelayCommand]
  async Task GoToCreateDiaryEntry()
  {
    await Shell.Current.GoToAsync($"{nameof(DiaryEntryFoodCreatePage)}", true, new Dictionary<string, object>
    {
      {"Food", SelectedFood},
      {"PageMode", FoodDetailsVM.PageMode.View}
    });
    SelectedFood = null;
  }

  [RelayCommand]
  private async void NewFood()
  {
    await Shell.Current.GoToAsync($"{nameof(FoodCreatePage)}", true, new()
    {
      { "PageMode", FoodDetailsVM.PageMode.Create }
    });
  }
}
