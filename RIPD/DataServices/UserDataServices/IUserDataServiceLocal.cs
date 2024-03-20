using RIPD.Models;

namespace RIPD.DataServices;

public interface IUserDataServiceLocal
{
  Task CreateAsync(User user);

  Task<User> GetFirstAsync();
}
