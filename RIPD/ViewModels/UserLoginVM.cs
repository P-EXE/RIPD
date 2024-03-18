using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
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
  [QueryProperty("Email", "Email"), QueryProperty("Password", "Password")]
  public partial class UserLoginVM : ObservableObject
  {
    private readonly IUserDataService _userDataService;

    [ObservableProperty]
    private string _email;
    [ObservableProperty]
    private string _password;

    public UserLoginVM(IUserDataService userDataService)
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
