using RIPD.Models;
using RIPD.Services;

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
  public async Task CreateOwnerAsync(Owner owner)
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
    }
    else
    {
      await _loc.CreateOwnerAsync(owner);
    }
    return;
  }
  public async Task<Owner?> LogInOwnerAsync(string email, string password)
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
    }
    else
    {
      return await _loc.LogInAsync(email, password);
    }
    return null;
  }
  public async Task<Owner?> GetOwnerAsync()
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
    }
    else
    {
      return await _loc.GetOwnerAsync();
    }
    return null;
  }
  public async Task DeleteOwnerAsync()
  {
    if (_apiStatusChecker.CheckAPI().Result)
    {
    }
    else
    {
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
