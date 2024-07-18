using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPDApp.Services.Run
{
    public class RunGpsLocation
    {
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;
    private List<Location> _locations = new List<Location>();

    public async Task GetCurrentLocation()
    {
      try
      {
        _isCheckingLocation = true;

        GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(5));

        _cancelTokenSource = new CancellationTokenSource();

        Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

        if (location != null)
          Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
      }
      // Catch one of the following exceptions:
      //   FeatureNotSupportedException
      //   FeatureNotEnabledException
      //   PermissionException
      catch (Exception ex)
      {
        // Unable to get location
      }
      finally
      {
        _isCheckingLocation = false;
      }
    }

    async void OnStartListening()
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
      _locations.Add(e.Location);
    }

    void OnStopListening()
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
}
