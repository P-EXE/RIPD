using CommunityToolkit.Mvvm.Input;
using RIPD.Models;
using RIPD.ViewModels;

namespace RIPD.Views;

public partial class FoodListFoodV : ContentView
{
  public FoodListFoodV()
  {
    InitializeComponent();
  }

  public FoodListFoodV(Food food)
  {
    InitializeComponent();
    BindingContext = new FoodListFoodVM(food);
  }
}