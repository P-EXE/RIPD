using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.Views.Banner
{
  public partial class BannerViewModel : ObservableObject
  {
    [ObservableProperty]
    string statusText;
    public string Status => statusText;

    public BannerViewModel()
    {
      Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
    }

    private void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
      statusText = e.NetworkAccess.ToString();
    }
  }
}
