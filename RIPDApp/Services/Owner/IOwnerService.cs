using RIPDShared.Models;

namespace RIPDApp.Services;

/// <summary>
/// Provides abstraction for common actions regarding the Owner a.k.a. the currently logged in User
/// </summary>
public interface IOwnerService
{
  Task RegisterAsync(AppUser_Create createUser);
  Task LoginAsync(AppUser_Create createUser);
  Task<bool> AutoLogin();
  Task<bool> LogoutAsync();
  Task<AppUser?> UpdateAsync(AppUser updateUser);
  Task<bool> DeleteAsync();
  Task<bool> CheckUserLoginStateAsync();
}
