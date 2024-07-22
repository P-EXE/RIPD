using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RIPDApp.DataBase;
using RIPDShared.Models;

namespace RIPDApp.Services;

public class OwnerService : IOwnerService
{
  private readonly IHttpService _httpService;
  private readonly LocalDBContext _localDBContext;
  private readonly IMapper _mapper;
  public OwnerService(IHttpService httpService, LocalDBContext localDBContext, IMapper mapper)
  {
    _httpService = httpService;
    _localDBContext = localDBContext;
    _mapper = mapper;
  }

  public async Task RegisterAsync(AppUser_Create createUser)
  {
    await _httpService.PostAsync("user/register", createUser);
  }

  public async Task LoginAsync(AppUser_Create createUser)
  {
    bool newBearerToken = false;
    bool newOwner = false;

    // Get Local Entities
    Statics.Auth.BearerToken = await _localDBContext.BearerTokens.FirstOrDefaultAsync();
    if (Statics.Auth.BearerToken == null)
    {
      newBearerToken = true;
    }
    Statics.Auth.Owner = await _localDBContext.Users.FirstOrDefaultAsync();
    if (Statics.Auth.Owner == null)
    {
      newOwner = true;
    }

    // Get Bearer Token
    try
    {
      Statics.Auth.BearerToken = await _httpService.PostAsync<AppUser_Create, BearerToken>("user/login", createUser);
    }
    catch (Exception ex)
    {
      throw;
    }

    // Inject Bearer Token to HTTPService
    await _httpService.Authorize();

    // Get Self
    try
    {
      Statics.Auth.Owner = await _httpService.GetAsync<AppUser>("user/self/private");
    }
    catch (Exception ex)
    {
      throw;
    }

    // Save Local Entities
    if (newBearerToken)
    {
      _localDBContext.BearerTokens.RemoveRange();
      await _localDBContext.BearerTokens.AddAsync(Statics.Auth.BearerToken);
    }
    if (newOwner)
    {
      _localDBContext.Users.RemoveRange();
      await _localDBContext.Users.AddAsync(Statics.Auth.Owner);
    }
    await _localDBContext.SaveChangesAsync();
  }

  public async Task<bool> DeleteAsync()
  {
    throw new NotImplementedException();
    return false;
  }

  public async Task<bool> AutoLogin()
  {
    // Get Bearer Token from Local
    Statics.Auth.BearerToken = await _localDBContext.BearerTokens.FirstOrDefaultAsync();
    if (Statics.Auth.BearerToken == null)
    {
      return false;
    }

    // Get Owner from Local
    Statics.Auth.Owner = await _localDBContext.Users.FirstOrDefaultAsync();

    // Get new Bearer Token from API
    Dictionary<string, string> refreshTokenPayload = new()
    {
      { "refreshToken", Statics.Auth.BearerToken.RefreshToken }
    };
    try
    {
      Statics.Auth.BearerToken = await _httpService.PostAsync<Dictionary<string, string>, BearerToken>("user/refresh", refreshTokenPayload);
    }
    catch (Exception ex)
    {
      return false;
    }
    await _httpService.Authorize();

    // Set the Owner from API
    try
    {
      Statics.Auth.Owner = await _httpService.GetAsync<AppUser>("user/self/private");
    }
    catch (Exception ex)
    {
      throw;
    }

    // Update Local
    await _localDBContext.SaveChangesAsync();

    return true;
  }

  public async Task<bool> LogoutAsync()
  {
    // Local DB
    if (Statics.Auth.Owner == null) return false;
    if (Statics.Auth.BearerToken == null) return false;
    await _localDBContext.Database.EnsureDeletedAsync();
    await _localDBContext.Database.EnsureCreatedAsync();

    // Statics
    Statics.Auth.Owner = null;
    Statics.Auth.BearerToken = null;

    // Return
    return true;
  }

  public async Task<AppUser?> UpdateAsync(AppUser user)
  {
    bool success = true;
    AppUser? retUser;
    // Mapping
    AppUser_Update updateUser = _mapper.Map<AppUser_Update>(user);

    try
    {
      retUser = await _httpService.PutAsync<AppUser_Update, AppUser>("user/update", updateUser);
    }
    catch (Exception ex)
    {
      success = false;
      throw;
    }

    if (success) Statics.Auth.Owner = retUser;

    return retUser;
  }

  public async Task<bool> CheckUserLoginStateAsync()
  {
    string? bt = await SecureStorage.Default.GetAsync("AccessToken");
    if (bt != null) return true;
    return false;
  }
}
