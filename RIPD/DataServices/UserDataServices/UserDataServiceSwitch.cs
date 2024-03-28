using RIPD.Models;
using RIPD.Services;
using System.Diagnostics;

namespace RIPD.DataServices;

public class UserDataServiceSwitch : IUserDataService
{
  private readonly UserDataServiceAPI _api;
  private readonly UserDataServiceLocal _loc;
  private readonly APIStatusChecker _apiStatusChecker;
  public UserDataServiceSwitch(UserDataServiceAPI api, UserDataServiceLocal loc, APIStatusChecker apiStatusChecker)
  {
    _api = api;
    _loc = loc;
    _apiStatusChecker = apiStatusChecker;
  }

  #region Owner
  /// <summary>
  /// Create a new User in the API DB and save it to the local DB as Owner
  /// </summary>
  /// <param name="owner"></param>
  /// <returns></returns>
  public async Task CreateOwnerAsync(Owner owner)
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
      Debug.WriteLine($"==CUSTOM=> UserDataServiceSwitch/CreateOwnerAsync: Hit Online");
      // Create User via API
      // Save User as Owner in local DB
    }
    else
    {
      Debug.WriteLine($"==CUSTOM=> UserDataServiceSwitch/CreateOwnerAsync: Hit Online");
      // For Debugging only
      await _loc.CreateOwnerAsync(owner);
    }
    return;
  }
  public async Task<Owner?> LogInOwnerAsync(string email, string password)
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
      // Check credentials and return the matching User as Owner
    }
    else
    {
      // For Debugging only
      return await _loc.LogInAsync(email, password);
    }
    return null;
  }
  public async Task<bool> LogOutOwnerAsync()
  {
    return false;
  }

  /// <summary>
  /// Fetch the owner from the Local DB
  /// </summary>
  /// <returns></returns>
  public async Task<Owner?> GetOwnerAsync()
  {
    return await _loc.GetOwnerAsync();
  }
  public async Task DeleteOwnerAsync()
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
      // Delete user in API DB & Local DB
    }
    else
    {
      // Only for local Debugging
      await _loc.DeleteOwnerAsync();
    }
    return;
  }
  #endregion Owner

  #region User
  public async Task<User?> GetUserByEmailAsync(string email)
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
    }
    else
    {
      return await _loc.GetUserByEmailAsync(email);
    }
    return null;
  }
  public async Task<IEnumerable<User?>> GetUserByNameAsync(string name)
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
    }
    else
    {
      return await _loc.GetUserByNameAsync(name);
    }
    return null;
  }
  #endregion User

  #region Inter
  public async Task FollowUserAsync(User user)
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
    }
    else
    {
      await _loc.FollowUserAsync(user);
    }
    return;
  }
  public async Task<IEnumerable<User?>> GetFollowingAsync()
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
    }
    else
    {
      await _loc.GetFollowingAsync();
    }
    return null;
  }
  #endregion Inter

  #region Debug
  public async Task CreateUserAsync(User user)
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
    }
    else
    {
      await _loc.CreateUserAsync(user);
    }
    return;
  }
  #endregion Debug
}
