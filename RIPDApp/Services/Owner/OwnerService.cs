using RIPDShared.Models;
using System.Diagnostics;

namespace RIPDApp.Services;

public class OwnerService : IOwnerService
{
  private IHttpService _httpService;
  public OwnerService(IHttpService httpService)
  {
    _httpService = httpService;
  }

  public async Task<bool> RegisterAsync(string email, string password)
  {
    AppUser_Create createUser = new() { Email = email, Password = password };

    bool result = await _httpService.PostAsync("user/register", createUser);
    return result;
  }

  public async Task<bool> LoginAsync(string email, string password)
  {
    AppUser_Create createUser = new() { Email = email, Password = password };

    BearerToken? bt = await _httpService.PostAsync<AppUser_Create, BearerToken>("user/login", createUser);
    if (bt != null)
    {
      Debug.WriteLine($"==Success==> {nameof(LoginAsync)} : Got {nameof(BearerToken)}");

      Statics.API.BearerToken = bt;
      /*await _httpService.Authorize();*/
      /*      await SecureStorage.Default.SetAsync("AccessToken", bt.AccessToken);
            await SecureStorage.Default.SetAsync("RefreshToken", bt.RefreshToken);*/
      return true;
    }
    return false;
  }

  public async Task<bool> DeleteAsync()
  {
    throw new NotImplementedException();
    return false;
  }

  public async Task<bool> LogoutAsync()
  {
    throw new NotImplementedException();
    return false;
  }

  public async Task<bool> CheckUserLoginStateAsync()
  {
    string? bt = await SecureStorage.Default.GetAsync("AccessToken");
    if (bt != null) return true;
    return false;
  }
}
