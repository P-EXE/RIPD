using CommunityToolkit.Mvvm.ComponentModel;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;
public partial class FoodListFoodVM : ObservableObject
{
  [ObservableProperty]
  private Food _food;

  public FoodListFoodVM(Food food)
  {
    _food = food;
  }
}
