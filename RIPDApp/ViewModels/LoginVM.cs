using RIPDApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;

namespace RIPDApp.ViewModels;

public partial class LoginVM : ObservableObject
{
  private readonly IUserService _userDataService;

  [ObservableProperty]
  private string? _email;
  [ObservableProperty]
  private string? _password;

  public LoginVM(IUserService userDataService)
  {
    _userDataService = userDataService;
  }

  [RelayCommand]
  private async Task LogIn()
  {
    /*await _userDataService.LogInOwnerAsync(Email, Password);*/
  }

  [RelayCommand]
  private async Task NavBack()
  {
    await Shell.Current.Navigation.PopAsync();
  }
}
