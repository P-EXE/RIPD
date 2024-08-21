using RIPDApi.Data;
using RIPDShared.Models;

namespace RIPDApi.Config;

public static class AuthConfig
{
  public static async Task RegisterAuthServices(this IServiceCollection services)
  {
    await ConfigureIdentity(services);
  }

  private static Task ConfigureIdentity(IServiceCollection services)
  {
    services.AddAuthorization();
    services.AddIdentityApiEndpoints<AppUser>()
      .AddEntityFrameworkStores<SQLDataBaseContext>();

    return Task.CompletedTask;
  }
}
