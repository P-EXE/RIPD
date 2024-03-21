using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.DataServices;
using RIPD.Pages;

namespace RIPD.ViewModels;

public partial class SettingsVM : ObservableObject
{
  private readonly IUserDataService _userDataService;

  public SettingsVM(IUserDataService userDataService)
  {
    _userDataService= userDataService;
  }

  [RelayCommand]
  private async Task GoToSettingsDev()
  {
    await Shell.Current.GoToAsync($"{nameof(SettingsDevPage)}", true);
  }
}
