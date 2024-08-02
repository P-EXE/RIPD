namespace RIPDApp.Services;

public interface IRunGpsLocationService
{
  Task<Location> GetCurrentLocation();
}
