using Microsoft.EntityFrameworkCore;
using RIPD.DataBase;
using RIPD.Models;
using System.Diagnostics;

namespace RIPD.DataServices;

public class UserDataServiceLocal
{
  private readonly LocalDBContext _localDBContext;

  public UserDataServiceLocal(LocalDBContext sqliteDBContext)
  {
    _localDBContext = sqliteDBContext;
  }

  #region Owner User
  public async Task CreateOwnerAsync(Owner owner)
  {
    try
    {
      await _localDBContext.Owner.AddAsync(owner);
    }
    catch (Exception addException)
    {
      Debug.WriteLine($"----> UserDataService/CreateAsync: Error while adding: {addException}");
      return;
    }
    try
    {
      await _localDBContext.SaveChangesAsync();
    }
    catch (Exception saveException)
    {
      Debug.WriteLine($"----> UserDataService/CreateAsync: Error while saving: {saveException}");
      return;
    }
  }
  public async Task LogInOwnerAsync(Owner owner)
  {
    try
    {
      await _localDBContext.Owner.AddAsync(owner);
    }
    catch(Exception addException)
    {
      Debug.WriteLine($"==Exception==> UserDataService / LogInOwnerAsync: Error while logging in Owner: {addException}");
    }
    try
    {
      await _localDBContext.SaveChangesAsync();
    }
    catch (Exception saveException)
    {
      Debug.WriteLine($"==Exception==> UserDataService / LogInOwnerAsync: Error while logging in Owner: {saveException}");
    }
  }
  public async Task<Owner?> GetOwnerAsync()
  {
    IQueryable<Owner> iqOwner;
    IQueryable<User> iqFollowing;
    Owner owner;
    List<User> following;

    try
    {
      // Get owner and instantiate
      iqOwner = _localDBContext.Owner.Select(o => o);
      owner = await iqOwner.FirstAsync();

      // Get contacts and instantiate
      iqFollowing = iqOwner.SelectMany(u => u.Following);
      following = iqFollowing.ToList();

      // Assign contacts to owner
      owner.Following = following;
      return owner;
    }
    catch (Exception e)
    {
      Debug.WriteLine($"----> UserDataService/GetOwnerAsync: Error while fetching Owner: {e}");
      return null;
    }
  }
  public async Task DeleteOwnerAsync(Owner owner)
  {
    try
    {
      _localDBContext.Remove(owner);
    }
    catch (Exception removeException)
    {
      Debug.WriteLine($"----> UserDataService/DeleteOwnerAsync: Error while removing Owner: {removeException}");
      return;
    }
    try
    {
      await _localDBContext.SaveChangesAsync();
    }
    catch (Exception saveException)
    {
      Debug.WriteLine($"----> UserDataService/DeleteOwnerAsync: Error while removing Owner: {saveException}");
      return;
    }
  }
  #endregion Owner User

  #region Other Users
  public async Task<User?> GetUserByEmailAsync(string email)
  {
    IQueryable<User> iQuser;
    User user = new();
    try
    {
      iQuser = from u in _localDBContext.Users
               where u.Email.Equals(email)
               select u as User;
      user = await iQuser.FirstAsync();
    }
    catch (Exception fetchException)
    {
      Debug.WriteLine($"----> UserDataService/GetByEmailAsync: Error while fetching user by email: {fetchException}");
      return null;
    }
    return user;
  }
  public async Task<IEnumerable<User?>> GetUserByNameAsync(string name)
  {
    IEnumerable<User> users;
    try
    {
      var iQusers = from u in _localDBContext.Users
                    where u.Name.Contains(name)
                    select u as User;
      users = iQusers.AsEnumerable();
    }
    catch (Exception fetchException)
    {
      Debug.WriteLine($"----> UserDataService/GetByNameAsync: Error while fetching users by name: {fetchException}");
      return null;
    }
    return users;
  }
  #endregion Other Users

  #region Inter User
  public async Task FollowUserAsync(User user)
  {
    User owner = await GetOwnerAsync();
    owner.Following.Add(user);
    _localDBContext.Update(owner);
    await _localDBContext.SaveChangesAsync();
  }
  public async Task<IEnumerable<User?>> GetFollowingAsync()
  {
    User owner = await GetOwnerAsync();
    return owner.Following;
  }
  #endregion Inter User

  #region Debug
  public async Task CreateUserAsync(User user)
  {
    try
    {
      await _localDBContext.AddAsync(user);
    }
    catch (Exception addException)
    {
      Debug.WriteLine($"----> UserDataService/CreateAsync: Error while adding: {addException}");
      return;
    }
    try
    {
      await _localDBContext.SaveChangesAsync();
    }
    catch (Exception saveException)
    {
      Debug.WriteLine($"----> UserDataService/CreateAsync: Error while saving: {saveException}");
      return;
    }
  }
  #endregion Debug
}
