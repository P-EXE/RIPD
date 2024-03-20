using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.DataServices;
using RIPD.Pages;

namespace RIPD.ViewModels;

public partial class SettingsVM : ObservableObject
{
  private readonly IUserDataServiceLocal _userDataServiceLocal;

  public SettingsVM(IUserDataServiceLocal userDataServiceLocal)
  {
    _userDataServiceLocal = userDataServiceLocal;
  }

  [RelayCommand]
  private async void GoToSettingsDev()
  {
    await Shell.Current.GoToAsync($"{nameof(SettingsDevPage)}", true);
  }
  [RelayCommand]
  private async void GoToRegisterPage()
  {
    await Shell.Current.GoToAsync($"{nameof(UserRegisterPage)}", true);
  }
  [RelayCommand]
  private async void GoToLoginPage()
  {
    await Shell.Current.GoToAsync($"{nameof(UserLoginPage)}", true);
  }
}
