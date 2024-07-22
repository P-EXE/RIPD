using RIPDApp.Services;
using RIPDShared.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RIPDApp.ViewModels;

[QueryProperty(nameof(User), nameof(User))]
[QueryProperty(nameof(ActivePageMode), nameof(PageMode))]
public partial class UserProfileVM : ObservableObject
{
  private readonly IOwnerService _ownerService;
  private readonly IUserService _userService;

  public UserProfileVM(IOwnerService ownerService, IUserService userService)
  {
    _ownerService = ownerService;
    _userService = userService;
  }

  // Page State Fields
  [ObservableProperty]
  private int _activePageMode;
  [ObservableProperty]
  private bool _pageModeForeignView;
  [ObservableProperty]
  private bool _pageModeOwnerView;
  [ObservableProperty]
  private bool _pageModeOwnerUpdate;

  [ObservableProperty]
  private AppUser? _user = Statics.Auth.Owner;

  [RelayCommand]
  private async Task UpdateUserInfo()
  {
    bool success = true;

    try
    {
      await _ownerService.UpdateAsync(User);
    }
    catch (Exception ex)
    {
      success = false;
      User = Statics.Auth.Owner;
      await Shell.Current.DisplayAlert("Error", ex.Message, "Return");
    }

    if (!success) return;
    await GoBack();
  }

  private async Task GoBack() => await Shell.Current.GoToAsync("..");

  #region PageMode
  async partial void OnActivePageModeChanged(int value)
  {
    switch ((PageMode)value)
    {
      case PageMode.ForeignView:
        {
          User = User;
          break;
        }
      case PageMode.OwnerView:
        {
          User = Statics.Auth.Owner;
          break;
        }
      case PageMode.OwnerUpdate:
        {
          break;
        }
    }
  }

  [RelayCommand]
  private async Task SwitchToUpdateMode()
  {
    await Shell.Current.GoToAsync(Routes.UserProfileUpdatePage, false, new()
    {
      { nameof(PageMode), PageMode.OwnerUpdate },
    });
  }

  public enum PageMode
  {
    ForeignView = 0,
    OwnerView = 1,
    OwnerUpdate = 2
  }
  #endregion PageMode
}
