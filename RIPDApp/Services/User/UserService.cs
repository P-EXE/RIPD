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
    if (string.IsNullOrEmpty(query))
    {
      return null;
    }

    IEnumerable<AppUser>? users;

    Dictionary<string, object> queries = new()
    {
      ["name"] = query,
      ["position"] = position,
    };

    users = await _httpService.GetAsync<IEnumerable<AppUser>?>("user", queries);

    return users;
  }
}
