using RIPDApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;

namespace RIPDApp.ViewModels;

public partial class LoginVM : ObservableObject
{
  private readonly IOwnerService _ownerService;

  [ObservableProperty]
  private string? _email = Statics.Auth.Email;
  [ObservableProperty]
  private string? _password = Statics.Auth.Password;

  public LoginVM(IOwnerService ownerService)
  {
    _ownerService = ownerService;
  }

  [RelayCommand]
  private async Task LogIn()
  {
    bool result = await _ownerService.LoginAsync(Email, Password);
    if (result)
    {
      await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
      return;
    }
    await Shell.Current.DisplayAlert("Error", "Could not register.", "Close");
    return;
  }
}
