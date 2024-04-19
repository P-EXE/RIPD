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

  [RelayCommand]
  private async Task GoToSettingsDev()
  {
    await Shell.Current.GoToAsync($"{nameof(SettingsDevPage)}", true);
  }
}
