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
  private readonly IUserService _userDataService;

  [ObservableProperty]
  private bool _isAvailable = true;
  [ObservableProperty]
  private string? _name;
  [ObservableProperty]
  private string? _displayName;
  [ObservableProperty]
  private string? _email;
  [ObservableProperty]
  private string? _password;

  public RegisterVM(IUserService userDataService)
  {
    _userDataService = userDataService;
  }

  [RelayCommand]
  private async Task Register()
  {
/*    IsAvailable = false;
    AppUser_DTOCreate owner;
    try
    {
      owner = new(Name, DisplayName, Email, Password);
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"----> RegisterVM/Register: Error while creating user model: {ex}");
      return;
    }
    bool success = await _userDataService.CreateOwnerAsync(owner);
    if (success)
    {
      await Shell.Current.Navigation.PopToRootAsync();
    }
    IsAvailable = true;
    return;*/
  }

  [RelayCommand]
  private async Task NavToLoginPage()
  {
    await Shell.Current.Navigation.PushAsync(new LoginPage(new LoginVM(_userDataService)));
  }
}
