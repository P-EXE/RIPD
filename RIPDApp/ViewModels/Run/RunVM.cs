using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services.Run;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPDApp.ViewModels.Run
{
  public partial class RunVM : ObservableObject
  {
    private readonly RunGpsLocation _location;

    [ObservableProperty]
    public List<Location> _locationsList = new List<Location>();

    public RunVM(RunGpsLocation location)
    {
      _location = location;
      LocationsList = _location.LocationsList;
    }

    [RelayCommand]
    async Task StartGettingLocation()
    {
      await _location.GetCurrentLocation();
    }
  }
}
