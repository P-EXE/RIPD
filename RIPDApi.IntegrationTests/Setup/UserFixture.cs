using RIPDShared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace RIPDApi.IntegrationTests.Setup;

public class UserFixture : DefaultFixture, IAsyncLifetime
{
  public string Password { get; private set; }
  public BearerToken BearerToken { get; private set; }
  public AuthenticationHeaderValue AuthenticationHeaderValue { get; private set; }
  public AppUser User { get; private set; }

  // Sync Setup
  public UserFixture()
  {

  }
  
  // Async Setup
  public async Task InitializeAsync()
  {
    Password = "P455w0rd!";

    Dictionary<string, string> credentials = new()
    {
      ["Email"] = "testuser@mail.com",
      ["Password"] = Password,
    };

    HttpResponseMessage registerResponse = await Client.PostAsJsonAsync("api/user/register", credentials, JsonOpt);
    registerResponse.EnsureSuccessStatusCode();

    HttpResponseMessage loginResponse = await Client.PostAsJsonAsync("api/user/login", credentials, JsonOpt);
    loginResponse.EnsureSuccessStatusCode();

    BearerToken? bt = await JsonSerializer.DeserializeAsync<BearerToken>(await loginResponse.Content.ReadAsStreamAsync(), JsonOpt);
    Assert.NotNull(bt);
    BearerToken = bt;

    AuthenticationHeaderValue = new(BearerToken.TokenType, BearerToken.AccessToken);

    Client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue;

    HttpResponseMessage selfResponse = await Client.GetAsync("api/user/self/private");
    selfResponse.EnsureSuccessStatusCode();

    AppUser? user = await JsonSerializer.DeserializeAsync<AppUser>(await selfResponse.Content.ReadAsStreamAsync(), JsonOpt);
    Assert.NotNull(user);

    User = user;
  }

  async Task IAsyncLifetime.DisposeAsync()
  {
    await DisposeAsync();
  }
}