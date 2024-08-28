using Amazon.Runtime.Internal.Util;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RIPDApp.Services;

public class RunGpsLocationService(ILogger<RunGpsLocationService> logger) : IRunGpsLocationService
{
  private readonly ILogger<RunGpsLocationService> _logger = logger;

  public event EventHandler<GeolocationLocationChangedEventArgs> LocationChanged;

  public async Task StartGettingLocationAsync()
  {
    _logger.LogInformation("Trying to start listening for foreground location updates");
    try
    {
      Geolocation.LocationChanged += Geolocation_LocationChanged;
      GeolocationListeningRequest request = new();
      bool success = await Geolocation.StartListeningForegroundAsync(request);

      string status = success ? "Started listening for foreground location updates" : "Couldn't start listening";
      _logger.LogInformation(status);

      return;
    }
    catch (Exception ex)
    {
      // Unable to start listening for location changes
      _logger.LogError("Unable to start listening for foreground location updates");
    }
  }

  public Task StopGettingLocationAsync()
  {
    try
    {
      Geolocation.LocationChanged -= Geolocation_LocationChanged;
      Geolocation.StopListeningForeground();
      string status = "Stopped listening for foreground location updates";
      _logger.LogInformation(status);
    }
    catch (Exception ex)
    {
      // Unable to stop listening for location changes
      _logger.LogError("Unable to stop listening for foreground location updates");
    }

    return Task.CompletedTask;
  }

  void Geolocation_LocationChanged(object? sender, GeolocationLocationChangedEventArgs e)
  {
    // Process e.Location to get the new location
    LocationChanged?.Invoke(sender, e);
    _logger.LogInformation("Obtained location update {e}", e);
  }
}
