using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using RIPDApp.Services;
using System.Collections.ObjectModel;

namespace RIPDApp.ViewModels
{
  public partial class RunVM : ObservableObject
  {

    private readonly IRunGpsLocationService _locationService;
    private readonly IDiaryService _diaryService;
    private readonly ILogger<RunVM> _logger;
    public RunVM(ILogger<RunVM> logger, IRunGpsLocationService locationService, IDiaryService diaryService)
    {
      _locationService = locationService;
      _diaryService = diaryService;
      _locationService.LocationChanged += OnLocationChanged;

      _logger = logger;
    }

    [ObservableProperty]
    private ObservableCollection<Location> _locations = [];
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotListening))]
    private bool _isListening = false;
    public bool IsNotListening => !IsListening;

    [RelayCommand]
    private async Task StartGettingLocation()
    {
      _logger.LogInformation("Clearing Locations Collection");
      Locations = [];
      _logger.LogInformation("Trying to start getting location");
      await _locationService.StartGettingLocationAsync();
      IsListening = true;
    }

    [RelayCommand]
    private async Task StopGettingLocation()
    {
      _logger.LogInformation("Trying to stop getting location");
      await _locationService.StopGettingLocationAsync();
      IsListening = false;
      _diaryService.AddRunEntryToDiaryAsync(_locations);
    }

    private void OnLocationChanged(object? sender, GeolocationLocationChangedEventArgs e)
    {
      _logger.LogInformation("Got new location");
      Locations.Add(e.Location);
    }
  }
}
