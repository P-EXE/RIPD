using RIPD.DataServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.Pages;

namespace RIPD.ViewModels;

public partial class LoginVM : ObservableObject
{
  private readonly IUserDataService _userDataService;

  [ObservableProperty]
  private string? _email;
  [ObservableProperty]
  private string? _password;

  public LoginVM(IUserDataService userDataService)
  {
    _userDataService = userDataService;
  }

  [RelayCommand]
  private async Task LogIn()
  {
    await _userDataService.LogInOwnerAsync(Email, Password);
  }

  [RelayCommand]
  private async Task NavBack()
  {
    await Shell.Current.Navigation.PopAsync();
  }
}
