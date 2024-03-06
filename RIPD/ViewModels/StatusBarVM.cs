using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.ViewModels
{
  public partial class StatusBarVM : ObservableObject
  {
    [ObservableProperty]
    private string _message;

    public StatusBarVM()
    {
      Message = Connectivity.NetworkAccess.ToString();
      Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
    }

    private void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
      switch (e.NetworkAccess)
      {
        default:
          Message = "Oops!";
          break;
        case NetworkAccess.Unknown:
          {
            Message = "Network Connection Unknown";
            break;
          }
        case NetworkAccess.None:
          {
            Message = "Network Connection None";
            break;
          }
        case NetworkAccess.Internet:
          {
            Message = "Network Connection Connected";
            break;
          }
      }
    }
  }
}
