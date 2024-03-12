using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.DataServices;
using RIPD.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  public partial class ProfilePageVM : ObservableObject
  {
    private readonly LocalDBContext _localDBContext;

    public ProfilePageVM(LocalDBContext localDBContext)
    {
      _localDBContext = localDBContext;
    }

    [RelayCommand]
    private async void GoToRegisterPage()
    {
      await Shell.Current.GoToAsync($"{nameof(UserRegisterPage)}", true);
    }

    [RelayCommand]
    private async void GoToLoginPage()
    {
      await Shell.Current.GoToAsync($"{nameof(UserLoginPage)}", true);
    }
  }
}
