using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.DataServices;
using RIPD.Models;
using System.Diagnostics;

namespace RIPD.ViewModels;

public partial class NewFoodVM : ObservableObject
{
  private readonly IFoodDataService _foodDataService;
  private readonly IUserDataService _userDataService;
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

  public NewFoodVM(IFoodDataService foodDataService, IUserDataService userDataService)
  {
    _foodDataService = foodDataService;
    _userDataService = userDataService;
  }

  [RelayCommand]
  private async Task CreateNewFood()
  {
    Debug.WriteLine("Starting to add food");
    try
    {
      Food_CreateDTO food = new()
      {
        Barcode = Barcode,
        Name = Name,
        Contributer = _userDataService.GetOwnerAsync().Result.Id,
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