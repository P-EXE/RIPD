using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.DataServices;
using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  public partial class UserLoginVM : ObservableObject
  {
    private readonly UserDataServiceAPI _userDataService;

    [ObservableProperty]
    private string _email;
    [ObservableProperty]
    private string _password;

    public UserLoginVM(UserDataServiceAPI userDataService)
    {
      _userDataService = userDataService;
    }

    [RelayCommand]
    private async Task Login()
    {
      User user = new();
      try
      {
        user = await _userDataService.GetOneAsync(("email",Email));
      }
      catch (Exception ex)
      {
        Debug.WriteLine("--> Custom(Error): UserLoginVM.Login: OOOPS!");
      }
    }
  }
}
