using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDApp.Pages;

namespace RIPDApp.ViewModels;

public partial class SettingsVM : ObservableObject
{
  private readonly IOwnerService _ownerService;

  public SettingsVM(IOwnerService ownerService)
  {
    _ownerService = ownerService;
  }

  [ObservableProperty]
  private bool _metric = true;

  [RelayCommand]
  private async Task LogOut()
  {
    bool success = await _ownerService.LogoutAsync();
    if (!success) return;
    await Shell.Current.GoToAsync($"///{nameof(RegisterPage)}");
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
