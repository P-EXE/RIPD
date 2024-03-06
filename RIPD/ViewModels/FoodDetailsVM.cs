using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels

{
  [QueryProperty("Food", "Food")]
  internal partial class FoodDetailsVM : ObservableObject
  {
    [ObservableProperty]
    private Food _food;

    [RelayCommand]
    async Task GoBack() => await Shell.Current.GoToAsync("..");
  }
}
