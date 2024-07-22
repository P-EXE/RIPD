using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using RIPDShared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

public class UserFixture : WebApplicationFactory<Program>, IDisposable
{
  public readonly HttpClient TestClient;
  public readonly AppUser TestUser;
  public readonly JsonSerializerOptions JsonOpt;

  public UserFixture()
  {
    TestClient = CreateClient();

    JsonOpt = new()
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    TestUser = TestUserSetup();
  }

  private AppUser TestUserSetup()
  {
    Dictionary<string, string> testUserLogin = new()
    {
      ["Email"] = Defaults.TESTUSER_EMAIL,
      ["Password"] = Defaults.TESTUSER_PASSWORD,
    };

    HttpResponseMessage register = TestClient.PostAsJsonAsync("api/user/register", testUserLogin, JsonOpt).Result;
    register.EnsureSuccessStatusCode();

    HttpResponseMessage login = TestClient.PostAsJsonAsync("api/user/login", testUserLogin, JsonOpt).Result;
    login.EnsureSuccessStatusCode();

    BearerToken? bt = JsonSerializer.Deserialize<BearerToken>(login.Content.ReadAsStringAsync().Result, JsonOpt);
    Assert.NotNull(bt);
    TestClient.DefaultRequestHeaders.Authorization = new(bt!.TokenType, bt.AccessToken);

    HttpResponseMessage self = TestClient.GetAsync("api/user/self/private").Result;
    self.EnsureSuccessStatusCode();

    AppUser? user = JsonSerializer.Deserialize<AppUser>(self.Content.ReadAsStringAsync().Result, JsonOpt);
    Assert.NotNull(user);
    return user;
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    base.ConfigureWebHost(builder);

    builder.ConfigureTestServices(services =>
    {
      services.SetupTestDB();
    });
    builder.UseEnvironment("Development");
  }

  public override ValueTask DisposeAsync()
  {
    return base.DisposeAsync();
  }
}