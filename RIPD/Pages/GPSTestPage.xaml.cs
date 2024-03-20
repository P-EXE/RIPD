namespace RIPD.Pages;

public partial class GPSTestPage : ContentPage
{
	
  public bool stst = false;

  public List<Location> locations = new List<Location>();

  public GPSTestPage()
  {
    InitializeComponent();
  }
  private async void OnCounterClicked(object sender, EventArgs e)
  {
    stst = true;

    while (stst)
    {
      await GetCurrentLocation();
      Thread.Sleep(2000);
    }
  }

  private void CounterBtn2_Clicked(object sender, EventArgs e)
  {
    stst = false;
  }

  private CancellationTokenSource _cancelTokenSource;
  private bool _isCheckingLocation;

  public async Task GetCurrentLocation()
  {
    try
    {
      _isCheckingLocation = true;

      GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));

      _cancelTokenSource = new CancellationTokenSource();

      Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

      if (location != null)
        LocationResult.Text = ($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
      locations.Add(location);
    }
    // Catch one of the following exceptions:
    //   FeatureNotSupportedException
    //   FeatureNotEnabledException
    //   PermissionException
    catch (Exception ex)
    {
      LocationResult.Text = ex.Message.ToString();
    }
    finally
    {
      _isCheckingLocation = false;
    }
  }

  public void CancelRequest()
  {
    if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
      _cancelTokenSource.Cancel();
  }
}