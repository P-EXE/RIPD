using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.Models;
using RIPD.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  public partial class FoodListFoodVM : ObservableObject
  {
    [ObservableProperty]
    private Food _food;

    public FoodListFoodVM(Food food)
    {
      _food = food;
    }
  }
}
