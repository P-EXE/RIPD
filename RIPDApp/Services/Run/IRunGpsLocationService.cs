namespace RIPDApp.Services;

public interface IRunGpsLocationService
{
  Task StartGettingLocationAsync();
  Task StopGettingLocationAsync();

  event EventHandler<GeolocationLocationChangedEventArgs> LocationChanged;
}
