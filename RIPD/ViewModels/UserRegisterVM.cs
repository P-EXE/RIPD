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
  public partial class UserRegisterVM : ObservableObject
  {
    private readonly UserDataService _userDataService;
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _displayName;
    [ObservableProperty]
    private string _email;
    [ObservableProperty]
    private string _password;

    public UserRegisterVM(UserDataService userDataService)
    {
      _userDataService = userDataService;
    }

    [RelayCommand]
    private async Task Register()
    {
      User user = new();
      try
      {
        user = new()
        {
          Name = Name,
          DisplayName = DisplayName,
          Email = Email,
          Password = Password
        };

        user = await _userDataService.GetUserAsync(Email);
        if (user == null)
        {
          await _userDataService.CreateUserAsync(user);
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("--> Custom(Error): UserRegisterVM.Register: Format incorrect!");
      }
      return;
    }
  }
}
