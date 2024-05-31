using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using RIPDApp.Messaging;
using RIPDApp.Pages;
using RIPDApp.Services;
using RIPDShared.Models;
using System.Collections.ObjectModel;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(ActivePageMode), nameof(PageMode))]
public partial class UserSearchVM : ObservableObject
{
  private readonly IUserService _userService;

  [ObservableProperty]
  private int _activePageMode;

  public UserSearchVM(IUserService userService)
  {
    _userService = userService;
  }

  [ObservableProperty]
  private bool _isRefreshing;
  [ObservableProperty]
  private string _searchText;
  [ObservableProperty]
  private ObservableCollection<AppUser>? _Users = [];
  [ObservableProperty]
  private AppUser? _selectedUser;

  [RelayCommand]
  async Task Search()
  {
    IEnumerable<AppUser>? users = await _userService.GetUsersByNameAtPositionAsync(SearchText, 0);
    Users = users?.ToObservableCollection();
  }

  [RelayCommand]
  async Task Refresh()
  {
  }

  [RelayCommand]
  async Task UserSelected()
  {
    switch ((PageMode)ActivePageMode)
    {
      case PageMode.View:
        {
          break;
        }
      case PageMode.Return:
        {
          ReturnSelection();
          break;
        }
    }
  }

  async Task ReturnSelection()
  {
    WeakReferenceMessenger.Default.Send(new PageReturnObjectMessage<AppUser>(SelectedUser));
    await Shell.Current.GoToAsync("..");
  }

  public enum PageMode
  {
    View = 0,
    Return = 1
  }
}
