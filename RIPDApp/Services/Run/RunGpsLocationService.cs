using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RIPDApp.Services;

public partial class RunGpsLocationService : IRunGpsLocationService
{
  private CancellationTokenSource _cancelTokenSource;
  private bool _isCheckingLocation;


  public ObservableCollection<Location> LocationsList = new ObservableCollection<Location>();

  public async Task<Location> GetCurrentLocation()
  {
    try
    {
      _isCheckingLocation = true;

      GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(5));

      _cancelTokenSource = new CancellationTokenSource();

      Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

      if (location != null)
      {
        LocationsList.Add(location);
        await OnStartListening();
        return location;
      }
      //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

    }
    // Catch one of the following exceptions:
    //   FeatureNotSupportedException
    //   FeatureNotEnabledException
    //   PermissionException
    catch (Exception ex)
    {
      // Unable to get location
      Debug.WriteLine(ex);
    }
    finally
    {
      _isCheckingLocation = false;
    }
    return null;
  }

  async Task OnStartListening()
  {
    try
    {
      Geolocation.LocationChanged += Geolocation_LocationChanged;
      var request = new GeolocationListeningRequest(GeolocationAccuracy.High);
      var success = await Geolocation.StartListeningForegroundAsync(request);

      string status = success
          ? "Started listening for foreground location updates"
          : "Couldn't start listening";
    }
    catch (Exception ex)
    {
      // Unable to start listening for location changes
      Debug.WriteLine(ex);
    }
  }

  void Geolocation_LocationChanged(object sender, GeolocationLocationChangedEventArgs e)
  {
    // Process e.Location to get the new location
    LocationsList.Add(e.Location);
  }

   async public Task OnStopListening()
  {
    try
    {
      Geolocation.LocationChanged -= Geolocation_LocationChanged;
      Geolocation.StopListeningForeground();
      string status = "Stopped listening for foreground location updates";
    }
    catch (Exception ex)
    {
      // Unable to stop listening for location changes
    }
  }

  public void CancelRequest()
  {
    if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
      _cancelTokenSource.Cancel();
  }
}
