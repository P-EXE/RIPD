using RIPDApp.Services;
using RIPDShared.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RIPDApp.ViewModels;

public partial class OwnerProfileVM : ObservableObject
{
  private readonly IOwnerService _userDataService;

  [ObservableProperty]
  private AppUser? _owner;

  public OwnerProfileVM(IOwnerService userDataService)
  {
/*    _userDataService = userDataService;
    Owner = _userDataService.GetOwnerAsync().Result;*/
  }

  [RelayCommand]
  private async Task DeleteAccount()
  {
/*    await _userDataService.DeleteOwnerAsync();
    await Shell.Current.Navigation.PopToRootAsync();*/
  }
}
