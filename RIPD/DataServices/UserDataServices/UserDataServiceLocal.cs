using Microsoft.EntityFrameworkCore;
using RIPD.Models;

namespace RIPD.DataServices;

internal class UserDataServiceLocal : IUserDataServiceLocal
{
  private readonly LocalDBContext _localDBContext;
  public UserDataServiceLocal(LocalDBContext localDBContext)
  {
    _localDBContext = localDBContext;
  }
  public async Task CreateAsync(User user)
  {
    _localDBContext.AddAsync(user);
  }
  public async Task<User> GetFirstAsync()
  {
    return await _localDBContext.Users.FirstAsync();
  }
}
