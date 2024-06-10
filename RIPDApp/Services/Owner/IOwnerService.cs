namespace RIPDApp.Services;

/// <summary>
/// Provides abstraction for common actions regarding the Owner a.k.a. the currently logged in User
/// </summary>
public interface IOwnerService
{
  /// <summary>
  /// Performs a register request to the Api
  /// </summary>
  /// <param name="email">The email of the User to log in</param>
  /// <param name="password">The password of the User to log in</param>
  /// <returns>The result of the operation as successful or unsuccessful</returns>
  Task<bool> RegisterAsync(string email, string password);

  /// <summary>
  /// Performs a login request to the Api.
  /// After a successfull login the returned Bearer Token is stored in the Statics Class, as well as the local DB.
  /// </summary>
  /// <param name="email">The email of the User to log in</param>
  /// <param name="password">The password of the User to log in</param>
  /// <returns>The result of the operation as successful or unsuccessful</returns>
  Task<bool> LoginAsync(string email, string password);
  Task<bool> LogoutAsync();
  Task<bool> DeleteAsync();
  Task<bool> CheckUserLoginStateAsync();
}
