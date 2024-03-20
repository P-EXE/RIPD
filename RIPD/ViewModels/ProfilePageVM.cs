using CommunityToolkit.Mvvm.ComponentModel;
using RIPD.DataServices;
using RIPD.Models;

namespace RIPD.ViewModels;

public partial class ProfilePageVM : ObservableObject
{
  private readonly IUserDataServiceLocal _userDataServiceLocal;
  [ObservableProperty]
  private User _activeUser;

  public ProfilePageVM(IUserDataServiceLocal userDataServiceLocal)
  {
    _userDataServiceLocal = userDataServiceLocal;
    ActiveUser = _userDataServiceLocal.GetFirstAsync().Result;
  }
}
