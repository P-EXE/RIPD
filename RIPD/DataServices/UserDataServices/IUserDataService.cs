using RIPD.Models;

namespace RIPD.DataServices;

public interface IUserDataService
{
  #region Owner
  Task CreateOwnerAsync(User_CreateDTO user);
  Task LogInOwnerAsync(string email, string password);
  Task LogOutOwnerAsync();
  Task<Owner?> GetOwnerAsync();
  Task DeleteOwnerAsync();
  #endregion Owner

  #region Users
  Task<User?> GetUserByEmailAsync(string email);
  Task<IEnumerable<User?>?> GetUserByNameAsync(string name);
  #endregion Users

  #region Inter
  Task FollowUserAsync(User user);
  Task<IEnumerable<User?>> GetFollowingAsync();
  #endregion Inter

  #region Debug
  Task CreateUserAsync(User user);
  #endregion Debug
}
