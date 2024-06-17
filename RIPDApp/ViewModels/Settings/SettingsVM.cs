using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDApp.Pages;

namespace RIPDApp.ViewModels;

public partial class SettingsVM : ObservableObject
{
  private readonly IUserService _userDataService;

  public SettingsVM(IUserService userDataService)
  {
    _userDataService= userDataService;
  }

  [ObservableProperty]
  private bool _metric = true;

  [RelayCommand]
  private async Task LogOut()
  {
    
  }

  [RelayCommand]
  private async Task DeleteAccount()
  {

  }

  [RelayCommand]
  private async Task GoToSettingsDev()
  {
    await Shell.Current.GoToAsync($"{nameof(SettingsDevPage)}", true);
  }
}
