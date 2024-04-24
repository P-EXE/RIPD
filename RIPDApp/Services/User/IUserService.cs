using RIPDShared.Models;

namespace RIPDApp.Services;

public interface IUserService
{
  Task<IEnumerable<AppUser>?> GetUsersByNameAtPositionAsync(string query, int position);
}
