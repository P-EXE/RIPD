using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(User), "user")]
public partial class ProfileVM : ObservableObject
{
  private readonly IUserService _userDataService;

  [ObservableProperty]
  private AppUser _user;
  [ObservableProperty]
  private bool _isAddable = false;

  public ProfileVM(IUserService userDataService)
  {
    _userDataService = userDataService;
  }

  [RelayCommand]
  private async Task AddToContacts()
  {
    /*await _userDataService.FollowUserAsync(User);*/
  }
}
