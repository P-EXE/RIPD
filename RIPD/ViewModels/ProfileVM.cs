using RIPD.DataServices;
using RIPD.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RIPD.ViewModels;

[QueryProperty(nameof(User), "user")]
public partial class ProfileVM : ObservableObject
{
  private readonly IUserDataService _userDataService;

  [ObservableProperty]
  private User _user;
  [ObservableProperty]
  private bool _isAddable = false;

  public ProfileVM(IUserDataService userDataService)
  {
    _userDataService = userDataService;
  }

  [RelayCommand]
  private async Task AddToContacts()
  {
    await _userDataService.FollowUserAsync(User);
  }
}
