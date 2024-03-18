using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPD.DataServices;
using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  public partial class StatusBarVM : ObservableObject
  {
    private readonly IUserDataServiceLocal _userdataServiceLocal;
    [ObservableProperty]
    private string _internetStatus;

    [ObservableProperty]
    private User _user;

    public StatusBarVM(IUserDataServiceLocal userDataServiceLocal)
    {
      InternetStatus = Connectivity.NetworkAccess.ToString();
      Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
      _userdataServiceLocal = userDataServiceLocal;
      User = _userdataServiceLocal.GetFirstAsync().Result;
    }

    private void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
      switch (e.NetworkAccess)
      {
        default:
          InternetStatus = "Oops!";
          break;
        case NetworkAccess.Unknown:
          {
            InternetStatus = "Network Connection Unknown";
            break;
          }
        case NetworkAccess.None:
          {
            InternetStatus = "Network Connection None";
            break;
          }
        case NetworkAccess.Internet:
          {
            InternetStatus = "Network Connection Connected";
            break;
          }
      }
    }
  }
}
