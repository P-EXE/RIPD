using RIPD.DataServices;
using RIPD.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RIPD.ViewModels;

public partial class OwnerProfileVM : ObservableObject
{
  private readonly IUserDataService _userDataService;

  [ObservableProperty]
  private Owner? _owner;

  public OwnerProfileVM (IUserDataService userDataService)
  {
    _userDataService = userDataService;
    Owner = _userDataService.GetOwnerAsync().Result;
  }

  [RelayCommand]
  private async Task DeleteAccount()
  {
    await _userDataService.DeleteOwnerAsync();
    await Shell.Current.Navigation.PopToRootAsync();
  }
}
