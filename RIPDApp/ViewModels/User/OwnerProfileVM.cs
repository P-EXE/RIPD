using RIPDApp.Services;
using RIPDShared.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RIPDApp.ViewModels;

public partial class OwnerProfileVM : ObservableObject
{
  private readonly IOwnerService _userDataService;
  public OwnerProfileVM()
  {

  }

  [ObservableProperty]
  private AppUser? _owner = Statics.Auth.Owner;
  [ObservableProperty]
  private int? _followers = Statics.Auth.Owner?.Followers?.Count();
  [ObservableProperty]
  private int? _following = Statics.Auth.Owner?.Following?.Count();

  public OwnerProfileVM(IOwnerService userDataService)
  {
    /*    _userDataService = userDataService;
        Owner = _userDataService.GetOwnerAsync().Result;*/
  }

  [RelayCommand]
  private async Task SwitchToEditMode()
  {
  }
}
