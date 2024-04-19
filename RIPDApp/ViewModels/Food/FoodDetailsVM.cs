using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

[QueryProperty("Food", "Food")]
public partial class FoodDetailsVM : ObservableObject
{
  public FoodDetailsVM()
  {
  }

  [ObservableProperty]
  private Food? _food;
}
