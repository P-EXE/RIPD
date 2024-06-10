using RIPDShared.Models;

namespace RIPDApp.Services;

public class UserService : IUserService
{
  private readonly IHttpService _httpService;
  public UserService(IHttpService httpService)
  {
    _httpService = httpService;
  }

  public async Task<IEnumerable<AppUser>?> GetUsersByNameAtPositionAsync(string query, int position)
  {
    IEnumerable<AppUser>? users;
    Dictionary<string, string> queries = new()
    {
      ["name"] = query,
      ["position"] = position.ToString(),
    };
    users = await _httpService.GetAsync<IEnumerable<AppUser>?>("user", queries);
    return users;
  }
}
