using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDShared.Models;

namespace RIPDApp.ViewModels;

public partial class UserSearchVM : ObservableObject
{
  private readonly IUserService _userService;

  public UserSearchVM(IUserService userService)
  {
    _userService = userService;
  }

  [ObservableProperty]
  private bool _isRefreshing;

  [ObservableProperty]
  private string _searchText;

  [ObservableProperty]
  private IEnumerable<AppUser>? _Users;

  [ObservableProperty]
  private AppUser? _selectedUser;

  [RelayCommand]
  async Task Search()
  {
    Users = await _userService.GetUsersByNameAtPositionAsync(SearchText, 0);
  }

  [RelayCommand]
  async Task Refresh()
  {
  }

  [RelayCommand]
  async Task ReturnSelection()
  {
    await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
    {
      ["Manufacturer"] = SelectedUser
    });
  }
}
