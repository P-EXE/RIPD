using RIPDShared.Models;

namespace RIPDApi.Repos;

public interface IUserRepo
{
  Task<AppUser?> GetSelfPublicAsync(AppUser? user);
  Task<AppUser?> GetSelfPrivateAsync(AppUser? user);
  Task<IEnumerable<AppUser>?> GetUsersByNameAtPosition(string name, int position);
}
