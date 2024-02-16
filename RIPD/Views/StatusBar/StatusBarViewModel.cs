using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.Views.StatusBar
{
  internal partial class StatusBarViewModel : ObservableObject
  {
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MessageV))]
    private string messageF;
    public string MessageV => messageF;

    public StatusBarViewModel()
    {
      messageF = Connectivity.NetworkAccess.ToString();
      Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
    }

    private void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
      switch (e.NetworkAccess)
      {
        default:
          MessageF = "Oops!";
          break;
        case NetworkAccess.Unknown:
          {
            MessageF = "Network Connection Unknown";
            break;
          }
        case NetworkAccess.None:
          {
            MessageF = "Network Connection None";
            break;
          }
        case NetworkAccess.Internet:
          {
            MessageF = "Network Connection Connected";
            break;
          }
      }
    }
  }
}
