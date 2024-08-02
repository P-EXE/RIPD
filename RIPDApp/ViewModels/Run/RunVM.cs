using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;
using System.Collections.ObjectModel;

namespace RIPDApp.ViewModels
{
  public partial class RunVM : ObservableObject
  {
    private readonly RunGpsLocationService _location;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentLocation))]
    private ObservableCollection<Location> _locationList;
    public Location? CurrentLocation => LocationList?.Last();
    public RunVM(RunGpsLocationService location)
    {
      _location = location;
    }

    [RelayCommand]
    async Task StartGettingLocation()
    {
      await _location.GetCurrentLocation();
      LocationList = _location.LocationsList;
    }

    [RelayCommand]
    async Task StopGettingLocation()
    {
      await _location.OnStopListening();
    }
  }
}
