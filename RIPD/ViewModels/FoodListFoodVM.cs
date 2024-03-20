using CommunityToolkit.Mvvm.ComponentModel;
using RIPD.Models;

namespace RIPD.ViewModels;
public partial class FoodListFoodVM : ObservableObject
{
  [ObservableProperty]
  private Food _food;

  public FoodListFoodVM(Food food)
  {
    _food = food;
  }
}
