using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RIPDApp.Services;

namespace RIPDApp.ViewModels
{
  public partial class RunVM : ObservableObject
  {
    private readonly RunGpsLocationService _location;

    [ObservableProperty]
    public List<Location> _locationsList = new List<Location>();

    public RunVM(RunGpsLocationService location)
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
