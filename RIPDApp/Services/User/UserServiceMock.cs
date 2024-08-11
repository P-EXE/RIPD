using RIPDShared.Models;

namespace RIPDApp.Services;

public class UserServiceMock : IUserService
{
  public Task<IEnumerable<AppUser>?> GetUsersByNameAtPositionAsync(string query, int position)
  {
    throw new NotImplementedException();
  }
}
