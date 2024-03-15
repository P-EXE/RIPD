using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.DataServices;
using RIPD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  public partial class NewFoodVM : ObservableObject
  {
    private IFoodDataService _foodDataService;
    [ObservableProperty]
    private string _barcode;
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private int _manufacturer;
    [ObservableProperty]
    private string _description;
    [ObservableProperty]
    private string _image;

    public NewFoodVM(IFoodDataService foodDataService)
    {
      _foodDataService = foodDataService;
    }

    [RelayCommand]
    private async Task CreateNewFood()
    {
      Debug.WriteLine("Starting to add food");
      try
      {
        Food food = new()
        {
          Id = -1,
          Barcode = Barcode,
          Name = Name,
          Manufacturer = Manufacturer,
          Description = Description,
          Image = Image,
          CreationDateTime = DateTime.Now
        };
        await _foodDataService.CreateAsync(food);
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
      }
      finally
      {
        
      }
      GoBack();
    }

    [RelayCommand]
    async Task GoBack() => await Shell.Current.GoToAsync("..");
  }
}