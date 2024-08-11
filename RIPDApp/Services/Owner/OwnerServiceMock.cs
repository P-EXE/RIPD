using RIPDShared.Models;

namespace RIPDApp.Services;

public class OwnerServiceMock : IOwnerService
{
  public Task<bool> AutoLogin()
  {
    return Task.FromResult(true);
  }

  public Task<bool> CheckUserLoginStateAsync()
  {
    throw new NotImplementedException();
  }

  public Task<bool> DeleteAsync()
  {
    throw new NotImplementedException();
  }

  public Task LoginAsync(AppUser_Create createUser)
  {
    throw new NotImplementedException();
  }

  public Task<bool> LogoutAsync()
  {
    throw new NotImplementedException();
  }

  public Task RegisterAsync(AppUser_Create createUser)
  {
    throw new NotImplementedException();
  }

  public Task<AppUser?> UpdateAsync(AppUser updateUser)
  {
    throw new NotImplementedException();
  }
}
