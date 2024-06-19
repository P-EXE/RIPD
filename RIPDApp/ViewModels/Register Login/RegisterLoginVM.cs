using RIPDApp.Services;
using RIPDApp.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;
public partial class RegisterLoginVM : ObservableObject
{
  private readonly IOwnerService _ownerService;

  [ObservableProperty]
  private bool _isAvailable = true;
  [ObservableProperty]
  private string? _email = Statics.Auth.Email;
  [ObservableProperty]
  private string? _password = Statics.Auth.Password;

  public RegisterLoginVM(IOwnerService ownerService)
  {
    _ownerService = ownerService;
  }

  [RelayCommand]
  private async Task Register()
  {
    AppUser_Create? createUser = await ValidateForm();
    if (createUser == null) { return; }

    try
    {
      await _ownerService.RegisterAsync(createUser);
    }
    catch (Exception ex)
    {
      await Shell.Current.DisplayAlert("Could not Register", ex.Message, "OK");
      return;
    }

    try
    {
      await _ownerService.LoginAsync(createUser);
    }
    catch (Exception ex)
    {
      await Shell.Current.DisplayAlert("Could not Login", ex.Message, "OK");
      return;
    }

    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    return;
  }

  [RelayCommand]
  private async Task LogIn()
  {
    AppUser_Create? createUser = await ValidateForm();
    if (createUser == null) { return; }

    try
    {
      await _ownerService.LoginAsync(createUser);
    }
    catch (Exception ex)
    {
      await Shell.Current.DisplayAlert("Could not Login", ex.Message, "OK");
      return;
    }

    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    return;
  }

  private async Task<AppUser_Create?> ValidateForm()
  {
    AppUser_Create createUser = new()
    {
      Email = Email,
      Password = Password
    };

    if (createUser.Email == "" || createUser.Password == "")
    {
      await Shell.Current.DisplayAlert("Invalid form", "Please enter your Email and Password", "retry");
      return null;
    }

    return createUser;
  }
}
