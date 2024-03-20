using CommunityToolkit.Mvvm.ComponentModel;
using RIPD.DataServices;
using RIPD.Models;

namespace RIPD.ViewModels;

public partial class StatusBarVM : ObservableObject
{
  private readonly IUserDataService _userdataService;
  [ObservableProperty]
  private string _internetStatus;

  [ObservableProperty]
  private User? _user;

  public StatusBarVM(IUserDataService userDataService)
  {
    InternetStatus = Connectivity.NetworkAccess.ToString();
    Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
    _userdataService = userDataService;
    User = _userdataService.GetOwnerAsync().Result;
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
