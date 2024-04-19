using RIPDApp.Services;
using RIPDApp.Models;
using RIPDApp.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;
public partial class RegisterVM : ObservableObject
{
  private readonly IOwnerService _ownerService;

  [ObservableProperty]
  private bool _isAvailable = true;
  [ObservableProperty]
  private string? _email = Statics.RegisterLogin.Email;
  [ObservableProperty]
  private string? _password = Statics.RegisterLogin.Password;

  public RegisterVM(IOwnerService ownerService)
  {
    _ownerService = ownerService;
  }

  [RelayCommand]
  private async Task Register()
  {
    bool result = await _ownerService.RegisterAsync(Email, Password);
    if (result)
    {
      await _ownerService.LoginAsync(Email, Password);
      await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
      return;
    }
    await Shell.Current.DisplayAlert("Error", "Could not register.", "Close");
    return;
  }
}
