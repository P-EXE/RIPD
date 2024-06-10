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
    if (bt == null)
    {
      return false;
    }

    Debug.WriteLine($"==Success==> {nameof(LoginAsync)} : Got {nameof(BearerToken)}");
    Statics.Auth.BearerToken = bt;
    await _httpService.Authorize();
    
    AppUser? owner = await _httpService.GetAsync<AppUser>("user/self/private");
    if (owner == null)
    {
      return false;
    }

    Debug.WriteLine($"==Success==> {nameof(LoginAsync)} : Got {nameof(owner)}");
    Statics.Auth.Owner = owner;

    return true;
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
