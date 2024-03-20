using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.Models;

namespace RIPD.ViewModels;

[QueryProperty("Food", "Food")]
internal partial class FoodDetailsVM : ObservableObject
{
  [ObservableProperty]
  private Food _food;

  [RelayCommand]
  async Task GoBack() => await Shell.Current.GoToAsync("..");
}
