using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.ApplicationModel.Communication;
using RIPDApp.DataBase;
using RIPDShared.Models;
using System.Diagnostics;

namespace RIPDApp.Services;

public class OwnerService : IOwnerService
{
  private readonly IHttpService _httpService;
  private readonly LocalDBContext _localDBContext;
  public OwnerService(IHttpService httpService, LocalDBContext localDBContext)
  {
    _httpService = httpService;
    _localDBContext = localDBContext;
  }

  public async Task RegisterAsync(AppUser_Create createUser)
  {
    await _httpService.PostAsync("user/register", createUser);
  }

  public async Task LoginAsync(AppUser_Create createUser)
  {
    // Get Bearer Token
    BearerToken? bt = await _httpService.PostAsync<AppUser_Create, BearerToken>("user/login", createUser);
    if (bt == null)
    {
    }
    Debug.WriteLine($"==Success==> {nameof(LoginAsync)} : Got {nameof(BearerToken)}");
    Statics.Auth.BearerToken = bt;

    // Inject Bearer Token to HTTPService
    await _httpService.Authorize();

    // Get Self
    AppUser? owner = await _httpService.GetAsync<AppUser>("user/self/private");
    if (owner == null)
    {
    }
    Debug.WriteLine($"==Success==> {nameof(LoginAsync)} : Got {nameof(owner)}");
    Statics.Auth.Owner = owner;

    // Save to Local DB
    await _localDBContext.BearerTokens.AddAsync(bt);
    await _localDBContext.Users.AddAsync(owner);
  }
  public async Task<bool> DeleteAsync()
  {
    throw new NotImplementedException();
    return false;
  }

  private async Task AutoLogin()
  {
    // Get Bearer Token from Local
    Statics.Auth.BearerToken = await _localDBContext.BearerTokens.FirstAsync();
    if (Statics.Auth.BearerToken == null)
    {
      throw new NullReferenceException(nameof(Statics.Auth.BearerToken));
    }

    // Get new Bearer Token from API
    Statics.Auth.BearerToken = await _httpService.PostAsync<string, BearerToken>("user/refresh", Statics.Auth.BearerToken.RefreshToken);

    await _httpService.Authorize();

    // Get the Owner from API
    Statics.Auth.Owner = await _httpService.GetAsync<AppUser>("user/self");

    // Update Local
    _localDBContext.BearerTokens.Update(Statics.Auth.BearerToken);
    _localDBContext.Users.Update(Statics.Auth.Owner);
    await _localDBContext.SaveChangesAsync();
  }

  public async Task<bool> LogoutAsync()
  {
    // Local DB
    if (Statics.Auth.Owner == null) return false;
    _localDBContext.Users.Remove(Statics.Auth.Owner);
    if (Statics.Auth.BearerToken == null) return false;
    _localDBContext.BearerTokens.Remove(Statics.Auth.BearerToken);
    await _localDBContext.SaveChangesAsync();

    // Statics
    Statics.Auth.Owner = null;
    Statics.Auth.BearerToken = null;

    // Return
    return true;
  }

  public async Task<bool> CheckUserLoginStateAsync()
  {
    string? bt = await SecureStorage.Default.GetAsync("AccessToken");
    if (bt != null) return true;
    return false;
  }
}
