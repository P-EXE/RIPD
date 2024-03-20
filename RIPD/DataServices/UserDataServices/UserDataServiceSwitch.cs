using RIPD.Models;

namespace RIPD.DataServices;

public class UserDataServiceSwitch : IUserDataService
{
  private readonly UserDataServiceAPI _api;
  private readonly UserDataServiceLocal _loc;
  const bool _online = false;
  public UserDataServiceSwitch(UserDataServiceAPI api, UserDataServiceLocal loc)
  {
    _api = api;
    _loc = loc;
  }

  #region Owner
  public async Task CreateOwnerAsync(Owner owner)
  {
    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
