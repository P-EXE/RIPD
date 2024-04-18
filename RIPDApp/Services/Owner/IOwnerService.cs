namespace RIPDApp.Services;

public interface IOwnerService
{
  Task<bool> RegisterAsync(string email, string password);
  Task<bool> LoginAsync(string email, string password);
  Task<bool> LogoutAsync();
  Task<bool> DeleteAsync();
  Task<bool> CheckUserLoginStateAsync();
}
