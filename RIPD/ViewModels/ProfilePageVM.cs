using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.DataServices;
using RIPD.Models;
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
    private readonly IUserDataServiceLocal _userDataServiceLocal;
    [ObservableProperty]
    private User _activeUser;

    public ProfilePageVM(IUserDataServiceLocal userDataServiceLocal)
    {
      _userDataServiceLocal = userDataServiceLocal;
      ActiveUser = _userDataServiceLocal.GetFirstAsync().Result;
    }
  }
}
