using AutoMapper;
using RIPD.Models;
using RIPD.Services;
using System.Diagnostics;

namespace RIPD.DataServices;

public class UserDataServiceSwitch : IUserDataService
{
  private readonly UserDataServiceAPI _api;
  private readonly UserDataServiceLocal _loc;
  private readonly APIStatusChecker _apiStatusChecker;
  private readonly IMapper _mapper;
  public UserDataServiceSwitch(UserDataServiceAPI api, UserDataServiceLocal loc, APIStatusChecker apiStatusChecker, IMapper mapper)
  {
    _api = api;
    _loc = loc;
    _apiStatusChecker = apiStatusChecker;
    _mapper = mapper;
  }

  #region Owner
  public async Task CreateOwnerAsync(User_CreateDTO user)
  {
    Debug.WriteLine($"==Status==> UserDataServiceSwitch / CreateOwnerAsync : Hit Online");
    User resultUser = await _api.CreateUserAsync(user);
    Owner owner = _mapper.Map<Owner>(resultUser);
    await _loc.CreateOwnerAsync(owner);
    Debug.WriteLine($"==Status==> UserDataServiceSwitch / CreateOwnerAsync : Done Online");
  }
  public async Task LogInOwnerAsync(string email, string password)
  {
    User user = await _api.GetUserByEmailAndPassword(email, password);
    Owner owner = _mapper.Map<Owner>(user);
    await _loc.LogInOwnerAsync(owner);
  }
  public async Task LogOutOwnerAsync()
  {
    Owner owner = await _loc.GetOwnerAsync();
    await _loc.DeleteOwnerAsync(owner);
  }

  public async Task<Owner?> GetOwnerAsync()
  {
    return await _loc.GetOwnerAsync();
  }
  public async Task DeleteOwnerAsync()
  {
    Owner owner = await _loc.GetOwnerAsync();
    await _api.DeleteUserAsync(owner.Id);
    await _loc.DeleteOwnerAsync(owner);
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
